import socket
# import json

# Three test images
with open('test1.jpg', 'rb') as data:
    image1 = data.read()
with open('test2.jpg', 'rb') as data:
    image2 = data.read()
with open('test3.jpg', 'rb') as data:
    image3 = data.read()

# Create a socket with IP, port
listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
listener.bind(('127.0.0.1', 10086))
# 1 means only allows one connections.
listener.listen(1)
print('Waiting for connect...')
# while connecting
client_executor, addr = listener.accept()
print('Connected to Unity')

# we need a loop to keep receving data from Unity
while(True):
    recv_str = client_executor.recv(1024).decode()
    print(recv_str)
    if(recv_str == "1"):
        client_executor.send(image1)   
    if(recv_str == "2"):
        client_executor.send(image2)   
    if(recv_str == "3"):
        client_executor.send(image3)   
    if(recv_str == "4"):
        break
# remember to close the socket
client_executor.close()
listener.close()
