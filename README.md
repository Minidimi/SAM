# Code for SAM visualization
Unity code for receiving ROS messages from the SAM robot and visualizing the SPIRIT scene.
## Requirements
+ PC with Windows and Unity (Tested with Windows 10 and Unity 2021.3.9f1)
+ Visual Studio (Tested with Visual Studio 2019)
+ [Setup of the Mixed Reality Toolkit in Unity](https://learn.microsoft.com/en-us/training/paths/beginner-hololens-2-tutorials/)
+ [Setup of Unity Robotics Hub in Unity and the on the ROS server](https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/ros_unity_integration/setup.md)
## Build and Deployment
To deploy the application on the hololens follow these steps:
+ Make sure the Hololens is connected to the same network as the PC used for deployment
+ To establish a connection to ROS, set the IP address and port of the ROS TCP server in Unity under the menu item Robotics -> ROS Settings
+ Build the Unity project under File -> Build Settings -> Universal Windows Platform -> Build
+ In the Build folder, select the generated .sln file in Visual Studio
+ Specify the Hololens IP adress under the Visual Studio menu item Debugging -> Debugging Properties -> Configuration Properties -> Debugging -> Device name
+ Make sure the Visual Studio solution is set to Release and ARM64 next to the launch button. If this is set, press the green launch button to launch the application on the hololens.
## Starting the ROS server
+ Navigate to the catkin workspace of the ROS TCP Endpoint.
+ Run the script 'devel/setub.bash'
+ Set the environment variables for 'ROS_IP' and 'ROS_MASTER_URI'
+ Run 'roslaunch ros_tcp_endpoint endpoint.launch' to launch the endpoint
## Remote Display of the Hololens View
+ To remotely display the view of the Hololens, enter the Hololens IP address in a browser
+ If a warning is shown, choose advanced, then continue despite risk
+ Enter the username and password for the Hololens
+ Go to Views -> Mixed Reality Capture
+ Choose Live Preview
## Add or Remove Models
To add a new model, convert it to a .obj file and add it into the scene. To move it through translations from ROS, add the RosManipulator Script. Here, make sure the origin coordinate system on the ROS side correctly corresponds to the position of the object. If this is not the case, the object can be added as the child of an empty GameObject corresponding to the coordinate system in ROS. The GameObject sam_scaler under Robot Box -> Bounding Area in the hierarchy represents the origin of the coordinate system. As such, all GameObjects of the SAM scene should be put underneath this in the scene hierarchy and then positioned in relation to the other objects. To receive the transformations from ROS, enter the topic name in the RosManipulator field of the corresponding GameObject.

To delete an object, simply remove it from the Unity scene. Here, it is important to consider hierarchies to not accidentally delete child objects.
