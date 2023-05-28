# Scheduler Simulator

Simulates a system's short-term scheduler. A WPF (Windows Presentation Foundation) desktop application implementing the MVVM (Model-View-ViewModel) pattern. 
<br/> 

Allows for personalized data input from the user, as well as to choose between different scheduling algorithms. 
The Strategy design pattern was implemented, allowing for easier enhancement of the application by facilitating the implementation of new scheduling algorithms. 
The pattern works by defining a family of algorithms, with each in a separate class, and making their objects interchangeable (even at runtime).
<br/> 

The "scheduler" folder contains the class library consisting of the business logic, while the folder "SchedulerWPF" contains the UI logic. 
<br/> 

<hr>

# Instructions

The image below shows the main window of the application. <br/>
The "Input Data" button allows the user to input custom data. <br/>
The "Next Clock Signal" button tells the simulator to advance to the next time instance; below this button the current time is displayed. <br/>
The process currently being executed by the CPU is shown under the buttons. <br/>
The "Processes In The System" field displays all the processes currently in the system simulation. <br/>
The "Log" field displays the operations made by the simulator at the last clock signal. <br/>
The "Queues" field displays all the queues the system uses to manage the processes in the system. <br/>

![Main Window](https://github.com/cadu1979/SchedulerSimulator/blob/main/instruction_imgs/1.png?raw=true)

Upon clicking the "Input Data" button, the window below comes up. <br/>
After choosing the number of processes created for the simulator, the button "Start Input" should be pressed. <br/>

![Data Input Window](https://github.com/cadu1979/SchedulerSimulator/blob/main/instruction_imgs/2.png?raw=true)

The image below shows an example of input with 5 processes. <br/>
The scheduling algorithm can also be chosen at this stage. <br/>
After all data has been entered, the button "Finish Input" must be pressed, closing this window. <br/>

![Input Example](https://github.com/cadu1979/SchedulerSimulator/blob/main/instruction_imgs/3.png?raw=true)

The image below shows the maind window of the application during execution.

![App Example](https://github.com/cadu1979/SchedulerSimulator/blob/main/instruction_imgs/4.png?raw=true)
