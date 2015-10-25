/*
19/08/2014
Servidor socket basico de exemplo para receber
e enviar mensagens ao cliente
*/ 

using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class servidor : MonoBehaviour {
	
	//declaracao das variaveis visiveis na classe
	GameObject meuservo;	
	private Socket cliente;
	TcpListener server = new TcpListener(IPAddress.Any, 7698); 
	string strMessage ="xxxx"; 

	// Use this for initialization
	void Start () {
		meuservo = GameObject.Find ("Servo1"); //associa objeto Cube com o script			
		server.Start();  // inicia o servidor		
	}
		
	// Update is called once per frame
	void Update () {		
		if (server.Pending()) 
		{
			//print ("comunica com cliente");
			cliente = server.AcceptSocket();
			cliente.Blocking = false;
			
			// operacoes de escrita e leitura com cliente
			byte [] mensagem = new byte[1024];
			cliente.Receive (mensagem); //leitura da mensagem enviada pelo cliente
			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding ();
			strMessage = encoding.GetString (mensagem).ToString (); // string contendo mensagem recebida do cliente
			//print (strMessage);

			//prepara mensagem para envio
			byte[] envioBuffer = new byte[4];
			envioBuffer[0] = (byte)'a';
			envioBuffer[1] = (byte)'c';
			envioBuffer[2] = (byte)'k';
			envioBuffer[3] = 10;
			cliente.Send(envioBuffer); //envia mensagem ao cliente	

		}	

		// utiliza a mensagem recebida do cliente que foi armazenada 
		// na variavel strMessage para atuar no objeto Cube da cena
		if (strMessage.Contains("noventa")) {
			meuservo.GetComponent<Servo_Motor_Limitado>().rotacao = 90; // rotaciona o servo em 90 graus
		}

	}
}