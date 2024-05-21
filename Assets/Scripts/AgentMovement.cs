using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class AgentMovement : Agent
{
    public GameOverScreen GameOverScreen;

    private float moveSpeed = 5.0f;
    private float fireForce = 20f;
    private float angle;
    public float maxHealth = 100.0f;
    public float currentHealth = 1.0f;
    public HealthBar healthBar;

    private bool m_Shoot;

    Vector2 arrowDir;

    private GameObject arrow;
    private GameObject target;
    //private GameObject[] lavaTiles;
    public GameObject bulletPrefab;
    List<Arrow> arrows = new();

    //public WeaponBow weapon;
    private float fireDelay = 2.0f;
    private float cooldown = 0.0f;
    // Start is called when game starts
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            GameOverScreen.GameOver(true);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Targets Local Position Sensors
        sensor.AddObservation(target.transform.localPosition.x);
        sensor.AddObservation(target.transform.localPosition.y);

        //Agents Local Position Sensors
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);

        //Projectiles Local Position Sensors
        if (arrow != null)
        {
            sensor.AddObservation(arrow.transform.localPosition.x);
            sensor.AddObservation(arrow.transform.localPosition.y);

        }
        else
        {
            sensor.AddObservation(0);
            sensor.AddObservation(0);
        }
    }

    public void MoveAgent(ActionBuffers actionBuffers)
    {
        var dirToGo = Vector2.zero;

        var continuousActions = actionBuffers.ContinuousActions;
        var discreteActions = actionBuffers.DiscreteActions;

        var forward = Mathf.Clamp(continuousActions[0], -1f, 1f);
        var right = Mathf.Clamp(continuousActions[1], -1f, 1f);
        angle = Mathf.Clamp(continuousActions[2], -1f, 1f) * 180;

        this.transform.localPosition += (new Vector3(forward, right) * moveSpeed * Time.deltaTime);

        arrowDir = DirectionFromAngle(angle);
        
        //Agent Shoot
        if (discreteActions[0] >= 3 )
        {
            m_Shoot = true;
        }

        if (m_Shoot && fireDelay < cooldown)
        {
            Shoot(actionBuffers, angle, arrowDir);
        }
    }

    public void Shoot(ActionBuffers actionBuffers, float arrowAngle, Vector2 arrowDir)
    {
        Vector3 ArrowGenerationPoint = new();

        ArrowGenerationPoint.x = transform.localPosition.x;
        ArrowGenerationPoint.y = transform.localPosition.y;
        ArrowGenerationPoint.z -= 1;

        //Draw Target and Shoot direction line
        //Debug.DrawRay(this.transform.position, target.transform.position - this.transform.position, Color.red, 2.0f);
        //Debug.DrawRay(this.transform.position, arrowDir * 5, Color.green, 2.0f);

        //Instantiate Arrow, Assign rotation and direction force
        arrow = Instantiate(bulletPrefab, transform.parent, false);
        arrow.transform.localPosition = ArrowGenerationPoint;
        arrow.transform.localRotation = Quaternion.Euler(0, 0, arrowAngle);

        arrows.Add(arrow.GetComponent<Arrow>());
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        //Fire shoot
        rb.AddForce(arrowDir.normalized * fireForce, ForceMode2D.Impulse);
        Arrow shootedArrow = arrow.GetComponent<Arrow>();

        //Check what has arrow hitted
        shootedArrow.OnHit += OnArrowHit;

        //Cooldown Reset
        cooldown = 0.0f;
    }

    private void OnArrowHit(bool hasHit, Arrow shootedArrow, GameObject targetHitted)
    {
        //Check hits and add rewards
        //if (hasHit)
        //{
        //    AddReward(30);
        //    m_Shoot = hasHit;
        //    Debug.Log("SHOOT TARGET");
        //    EndEpisode();
        //}
        //else
        //{
        //    if (targetHitted.gameObject.CompareTag("Lava"))
        //    {
        //        AddReward(-3);
        //        Debug.Log("SHOOT LAVA");
        //        //EndEpisode();
        //    }
        //    else
        //    {
        //        AddReward(-3);
        //        Debug.Log("SHOOT MISS");
        //        //EndEpisode();
        //    }
        //}
    }
    public void RandomPositions()
    {
        var localMinX = -7.823f;
        var localMaxX = 7.106f;
        var localMinY = 4.091f;
        var localMaxY = -3.6f;

        var localMinRandom = new Vector2(localMinX, localMinY);
        var localMaxRandom = new Vector2(localMaxX, localMaxY);

        //Set agent random Position
        this.transform.localPosition = new Vector2(Random.Range(localMinRandom.x, localMaxRandom.x), Random.Range(localMinRandom.y, localMaxRandom.y));

        //Set targets random positions
        target.transform.localPosition = new Vector2(Random.Range(localMinRandom.x, localMaxRandom.x), Random.Range(localMinRandom.y, localMaxRandom.y));
    }

    public Vector3 DirectionFromAngle(float _angle)
    {
        _angle = _angle * Mathf.Deg2Rad;
        Vector3 target = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0);
        return (transform.forward + target).normalized;
    }
    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //currentHealth = Mathf.Lerp(currentHealth, damage, 2.0f);
        healthBar.SetHealth(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            //Debug.Log("LAVA COLLISION");
            //AddReward(-5);
            //EndEpisode();
            TakeDamage(0.3f);

        }
        if (collision.gameObject.CompareTag("Target"))
        {
            //TakeDamage(0.3f);
            //Debug.Log("TARGET COLLISION");
            //AddReward(-5);
            //EndEpisode();
        }
        if (collision.gameObject.CompareTag("Arrow"))
        {
            //Debug.Log("TAKEN SHOT");
            //AddReward(-5);
            //EndEpisode();
            TakeDamage(20.0f);
        }
    }

    public override void OnEpisodeBegin()
    {
        m_Shoot = false;

        //Target Sensors
        if (target == null)
            target = transform.parent.GetChild(0).gameObject;

        RandomPositions();
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //Start cooldown time
        cooldown += Time.deltaTime;

        MoveAgent(actionBuffers);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //Heuristic Code
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        //Moving
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: continuousActions[0] = 1; break;
            case 0: continuousActions[0] = 0; break;
            case 1: continuousActions[0] = 2; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: continuousActions[1] = 1; break;
            case 0: continuousActions[1] = 0; break;
            case 1: continuousActions[1] = 2; break;
        }

        //On shoot get angle
        //continuousActions[2] = angle;


        //Shooting
        //shooted = 1 :: not shooted == 0;
        //discreteActions[0] = 1;
    }
}
