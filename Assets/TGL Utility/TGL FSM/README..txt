Created a FSM implementation to allow for easier state control
- IStateInterface is interface from which all States class will be eventually derived, it handles many of Monobehavoiur class Methods and anyone can add on it as needed
- AbsState is an abstract class that will be used to inherit from when a new state class has to be created. Each state class can be used to get the machine it controlls by GetMachine() method
- IStateMachineInterface interface has methods that defines how a state machine will have changes in it's states
- AbsStateMachine is an abstract class used to define few of the methods use of IStateMachineInterface and all your state machines must inherit from it.

This Utility is created by using old codes shared by friends and some online searches. 
If you feel it is copy of your personal creation, please reach out to me on linkedIn or UnityConnect on the below mentioned urls:
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/