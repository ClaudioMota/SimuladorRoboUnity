import socket
import time
import sys

andandoD = False
andandoE = False

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
 return data

def passoAFrente():
	enviar_para_unity("MD50")
	enviar_para_unity("ME130")
	time.sleep( 1 )
	enviar_para_unity("LD70")
	enviar_para_unity("LE110")
	time.sleep( 1 )
	enviar_para_unity("MD130")
	enviar_para_unity("ME50")
	time.sleep( 1 )
	enviar_para_unity("LD110")
	enviar_para_unity("LE70")
	time.sleep( 1 )
	enviar_para_unity("MD90")
	enviar_para_unity("ME90")
	time.sleep( 1 )

def passoAEsquerda():
	enviar_para_unity("MD60")
	enviar_para_unity("ME60")
	time.sleep( 1 )
	enviar_para_unity("LD70")
	enviar_para_unity("LE110")
	time.sleep( 1 )
	enviar_para_unity("MD120")
	enviar_para_unity("ME120")
	time.sleep( 1 )
	enviar_para_unity("LD110")
	enviar_para_unity("LE70")
	time.sleep( 1 )
	enviar_para_unity("MD90")
	enviar_para_unity("ME90")
	time.sleep( 1 )
	
def passoADireita():
	enviar_para_unity("MD120")
	enviar_para_unity("ME120")
	time.sleep( 1 )
	enviar_para_unity("LD70")
	enviar_para_unity("LE110")
	time.sleep( 1 )
	enviar_para_unity("MD60")
	enviar_para_unity("ME60")
	time.sleep( 1 )
	enviar_para_unity("LD110")
	enviar_para_unity("LE70")
	time.sleep( 1 )
	enviar_para_unity("ME90")
	time.sleep( 1 )
	
def converta(a):
    i = 0
    j = 0
    while(a[i]!='\n'):
        j += j*10 + int(a[i])
        i += 1
    return j

def distancia():
	return converta(enviar_para_unity("I"))

def checarLadoLivre():
    enviar_para_unity("PH170")
    time.sleep( 1 )
    dist1 = distancia() #mensagem enviada para o servidor unity
    enviar_para_unity("PH10")
    time.sleep( 1 )
    dist2 = distancia #mensagem enviada para o servidor unity
    enviar_para_unity("PH90")
    time.sleep( 1 )
    if(dist1 > dist2):
    	return 1
    return 2

DLATERAL = 22
DFRONTAL = 24
DSEMAFORO = 90

"direita()"
"esquerda()"
while(True):
	passoAFrente()
	dist = distancia()
	if(dist < 50):
		lado = checarLadoLivre()
		if(lado == 2):
			while(dist < 90):
				dist = distancia()
				passoADireita()
		else:
			while(dist < 90):
				dist = distancia()
				passoAEsquerda()
