import socket
import time
import sys

def enviar_para_unity (msg):
 TCP_IP = '127.0.0.1'
 TCP_PORT = 12598 #porta do servidor Unity
 BUFFER_SIZE = 1024 
 s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
 s.connect((TCP_IP, TCP_PORT))
 s.send(msg)
 data = s.recv(BUFFER_SIZE)
 s.close()
 print "mensagem:", data

def direita (i):
	while(i>0):
		i-=1
		enviar_para_unity("D") #mensagem enviada para o servidor unity
def esquerda(i):
	while(i>0):
		i-=1
		enviar_para_unity("E") #mensagem enviada para o servidor unity
def cima(i):
	while(i>0):
		i-=1
		enviar_para_unity("C") #mensagem enviada para o servidor unity
def baixo(i):
	while(i>0):
		i-=1
		enviar_para_unity("B") #mensagem enviada para o servidor unity
def abre(i):
	while(i>0):
		i-=1
		enviar_para_unity("A") #mensagem enviada para o servidor unity
def fecha(i):
	while(i>0):
		i-=1
		enviar_para_unity("F") #mensagem enviada para o servidor unity
def frente(i):
	while(i>0):
		i-=1
		enviar_para_unity("Y") #mensagem enviada para o servidor unity
def traz(i):
	while(i>0):
		i-=1
		enviar_para_unity("T") #mensagem enviada para o servidor unity
while(True):
	abertura = 43
	altura = 36
	
	time.sleep( 1 )
	esquerda(20) #mensagem enviada para o servidor unity
	time.sleep( 1 )
	abre(5) 
	time.sleep( 1 )
	baixo(altura) 
	time.sleep( 1 )
	fecha(abertura) 
	time.sleep( 1 )
	cima(altura) 
	time.sleep( 1 )
	direita(20)
	time.sleep( 1 )
	baixo(altura) 
	time.sleep( 1 )
	abre(abertura) 
	time.sleep( 1 )
	cima(altura) 
	time.sleep( 1 )
	fecha(5)
	abre(5)
	baixo(altura)
	time.sleep( 1 )
	fecha(abertura)
	time.sleep( 1 )
	cima(altura)
	time.sleep( 1 )
	esquerda(20)
	time.sleep( 1 )
	baixo(altura)
	time.sleep( 1 )
	abre(abertura)
	time.sleep( 1 )
	cima(altura)
	time.sleep( 1 )
	fecha(5)
	direita(20)
