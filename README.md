# Socket-Between-Unity-and-Python
Simple scripts to transfer image and string between python and Unity


## Python
  In Python, we create a socket with IP, port number and listen to coming connection from Unity.
  
  ![](https://github.com/Cqyhid/Socket-Between-Unity-and-Python/blob/main/1.PNG)
  
  Next, once we receive an connection request from Unity, we will accept the request and form a connection.
  
  ![](https://github.com/Cqyhid/Socket-Between-Unity-and-Python/blob/main/2.PNG)
  
  Then we need a loop to keep receving data from Unity. Otherwise, the 'client_executor' will only receive 
  data once. `client_executor.recv(1024)` means receive a data with buffsize 1024. After we received data 
  from Unity, we decode the Bytes data and turn it into string. Then based on the string content we can send
  data from python to Unity.
  
  ![](https://github.com/Cqyhid/Socket-Between-Unity-and-Python/blob/main/3.PNG)
  
***
## Unity
  In Unity, we make a simple UI to handle the incoming image, and four buttons to send information once we 
  click it.
  
  ![](https://github.com/Cqyhid/Socket-Between-Unity-and-Python/blob/main/4.PNG)
  
  In `Start()` we also create a socket with same IP and port number.Once we connect to Python, we also need
  to continuously receive data from python. Here we use a `Thread` to handle the data receiving.
  
  ![](https://github.com/Cqyhid/Socket-Between-Unity-and-Python/blob/main/5.PNG)
  
  Remember the transfer data is `byte` data. So we need to turn it into another format or turn to byte before
  sending the message.
  
  ![](https://github.com/Cqyhid/Socket-Between-Unity-and-Python/blob/main/6.PNG)
***
  
  You may find details information from codes. Thanks.
