/*--------------------------------
 * SevidorCena12.cs ??/??/2015
 * Definicao: Script utilitario responsavel
 * pelo controle do ambiente da Cena12 via rede, esse
 * controle consiste em direcionar motores 
 * do braço robotico presente no ambiente.
 * Uso: Usado para teste da Cena12.
 *-------------------------------- 
*/

using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ServidorCena12 : MonoBehaviour {
	//parte da rede
	TcpListener server = new TcpListener(IPAddress.Any, 12598);
	private Socket cliente;
	byte [] mensagem;

	//parte do cenario
	private Servo_Motor_Limitado servo1;  //variaveis que guardarao as referencias dos objetos relativos a
	private Servo_Motor_Limitado servo2;
	private Servo_Motor_Limitado servo3;  //cada servo motor limitado do manipulador robotico
	private Servo_Motor_Limitado servo4;
	private Servo_Motor_Limitado servo5;
	private bool loc; // variavel auxiliar para previnir duplo clique ao pressionar determinados botoes do teclado

	// Use this for initialization
	void Start () {
		servo1 = GameObject.Find("Servo1").GetComponent<Servo_Motor_Limitado>(); //busca o objeto referente ao servo motor 
		servo2 = GameObject.Find("Servo2").GetComponent<Servo_Motor_Limitado>();
		servo3 = GameObject.Find("Servo5").GetComponent<Servo_Motor_Limitado>(); //limitado no ambiente e referencia o mesmo com a variavel
		servo4 = GameObject.Find("Servo4").GetComponent<Servo_Motor_Limitado>();
		servo5 = GameObject.Find("Servo3").GetComponent<Servo_Motor_Limitado>();
		server.Start();  // inicia o servidor
	}
	
	
	// funcao de atualizacao responsavel por toda a logica de controle e rede
	void Update () {
		int tamanho = 0;
		if (server.Pending()){
			cliente = server.AcceptSocket(); //aceita conexao
			cliente.Blocking = false;

			mensagem = new byte[1024];
			tamanho = cliente.Receive (mensagem); //leitura da mensagem enviada pelo cliente
			//System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding ();
			//mensagem = encoding.GetString (mensagem).ToString ();

			byte[] envioBuffer = new byte[4];
			envioBuffer[0] = (byte)'a';
			envioBuffer[1] = (byte)'c';  // mensagem a ser enviada ao cliente
			envioBuffer[2] = (byte)'k';
			envioBuffer[3] = 10;
			cliente.Send(envioBuffer); //envia mensagem ao cliente	
		}	
		int i;
		for(i=0;i<tamanho;i++){
			switch((char)mensagem[i])
			{
				case 'C': //cima
					servo2.rotacao-=1;
				break;
				case 'B': //baixo
					servo2.rotacao+=1;
				break;
				case 'E': //esquerda
					servo1.rotacao-=1;
				break;
				case 'D': //direita
					servo1.rotacao+=1;	
				break;
				case 'Y': // segue a frente
					servo5.rotacao-=1;
				break;
				case 'T': // vai para traz
					servo5.rotacao+=1;
				break;
				case 'A': // abre
					servo3.rotacao+=1;
					servo4.rotacao-=1;	
				break;
				case 'F': //fecha
					servo3.rotacao-=1;
					servo4.rotacao+=1;	
				break;
			}
		}
	}
}
