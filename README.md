# AI MiniGame - Player vs AI 2D Shooting

This project is a 2D shooting game where you can train an AI opponent using Proximal Policy Optimization (PPO), a reinforcement learning algorithm.

# Getting Started
To set up this project, you'll need the following:

Unity 2022.3.6f1  [Unity Download](https://unity.com/download)
ML Agents[ML-Agents for Unity](https://github.com/topics/unity-ml-agents)

Installing ML-Agents:

Download the ML-Agents package from the GitHub repository.
Follow the installation guide: [Instalation Guide](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation.md)

# Training a New Model:

Open the AITraining scene in Unity.
Use the ML-Agents interface to start training the AI.
If you're unfamiliar with ML-Agents, refer to the getting started tutorial: [Getting Started Guide](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Getting-Started.md)

# Configuration:

The configuration file used for training is located at `Assets/ML-Agents/configuration.yaml` within the project folder.

# Starting Training:

The instructions you provided are a good starting point for training the AI in your project. Here's a breakdown of the steps involved:

Open a Terminal: Launch a command prompt or terminal window on your computer.

Navigate to Project Directory: Use the cd command to navigate to the folder where you cloned this repository.

Run Training Command: Execute the following command, replacing `shootTraining` with your desired unique name for this training session:

```mlagents-learn Assets/ML-Agents/configuration.yaml --run-id=shootTraining```

This command tells mlagents-learn to use the `Assets/ML-Agents/configuration.yaml` file for training and assigns the run ID shootTraining for identification.

# _Here's a breakdown of the connection:_

In ML-Agents, the Behavior Name of your agent in the training environment needs to match the base name of the corresponding YAML configuration file. This ensures that the training process uses the correct settings for your agent's behavior.

YAML Configuration File: This file defines the hyperparameters and training parameters for your agent's behavior. It usually resides in the config folder within your project or the ml-agents repository (depending on your setup).
Behavior Name: This is a property assigned to your agent in the Unity scene. It essentially identifies the specific behavior the agent should follow during training.
Example:

You have a YAML configuration file named `Assets/ML-Agents/configuration.yaml`
In your Unity scene, the agent you want to train has a BehaviorParameters component attached.
Within the BehaviorParameters component, you should set the Behavior Name property to "configuration" (without the .yaml extension).
By matching these names, ML-Agents knows which configuration file to use when training your agent. This ensures that the training process applies the correct hyperparameters and settings for the desired behavior.

Start Training in Unity: Once the command finishes executing, you'll see a message prompting you to "Start training by pressing the Play button in the Unity Editor."

Open Unity and Start Training:

Switch to your Unity project containing the AITraining scene.
Press the Play button (▶️) in the Unity Editor to initiate the training process.

# Built With

Unity 3D
ML-Agents library
Contributing
We welcome contributions to this project! If you have any improvements or suggestions, feel free to submit a pull request.

# License
This project is free to use under [MIT License](https://opensource.org/license/mit). Please refer to the LICENSE file for details.

# Contact
If you have any questions or feedback, feel free to reach out to me:

[GIT](https://github.com/isaacfurieri)

[LinkedIn](https://www.linkedin.com/in/isaac-furieri-19788474/)
