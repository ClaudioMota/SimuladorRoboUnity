/*--------------------------------
 * SevidorCena11.cs ??/??/2014
 * Definicao: Script utilitario responsavel
 * pelo controle do ambiente da Cena11 via rede, esse
 * controle consiste em direcionar motores 
 * e fazer leitura dos sensores do robô presente 
 * no ambiente.
 * Uso: Usado para teste da Cena11.
 *-------------------------------- 
*/

using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ServidorCena11 : MonoBehaviour {
	//parte da rede
	TcpListener server = new TcpListener(IPAddress.Any, 12598);
	private Socket cliente;
	byte [] mensagem;

	//parte do cenario
	private Sensor_Infra_Vermelho IV;
	private Servo_Motor_Limitado SPescoco1;  //variaveis que guardarao as referencias dos objetos relativos a
	private Servo_Motor_Limitado SPescoco2;
	private Servo_Motor_Limitado SBracoE;  //cada servo motor limitado do manipulador robotico
	private Servo_Motor_Limitado SBracoD;
	private Servo_Motor_Limitado SLevantarE;
	private Servo_Motor_Limitado SLevantarD;
	private bool loc; // variavel auxiliar para previnir duplo clique ao pressionar determinados botoes do teclado

	// Use this for initialization
	void Start () {
		//busca o objeto referente ao servo motor
		IV = GameObject.Find("IV").GetComponent<Sensor_Infra_Vermelho>();
		SPescoco1 = GameObject.Find("MovimetoPescoco").GetComponent<Servo_Motor_Limitado>();  //variaveis que guardarao as referencias dos objetos relativos a
		SPescoco2 = GameObject.Find("MovimentoPescoco2").GetComponent<Servo_Motor_Limitado>();
		SBracoE = GameObject.Find("MovimentoBracoE").GetComponent<Servo_Motor_Limitado>();  //cada servo motor limitado do manipulador robotico
		SBracoD = GameObject.Find("MovimentoBracoD").GetComponent<Servo_Motor_Limitado>();
		SLevantarE = GameObject.Find("MovimentoLevantarE").GetComponent<Servo_Motor_Limitado>();
		SLevantarD = GameObject.Find("MovimentoLevantarD").GetComponent<Servo_Motor_Limitado>();
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

			/*byte[] envioBuffer = new byte[4];
			envioBuffer[0] = (byte)'a';
			envioBuffer[1] = (byte)'c';  // mensagem a ser enviada ao cliente
			envioBuffer[2] = (byte)'k';
			envioBuffer[3] = 10;
			cliente.Send(envioBuffer); //envia mensagem ao cliente*/	
		}	
		int i;

		string palavra;
		byte [] envioBuffer = new byte[15];
		int j;
		for(i=0;i<tamanho;i++){
			switch((char)mensagem[i])
			{
			    case 'I':;
				     palavra = ((int)IV.GetDistancia()).ToString();
					 
				     for(j = 0;j<palavra.Length;j++){
					     envioBuffer[j] = (byte)palavra[j];  // mensagem a ser enviada ao cliente
					 }
					 envioBuffer[j] = 10;
					 cliente.Send(envioBuffer); //envia mensagem ao cliente	
				break;
				case 'L':
					i++;
					if(mensagem[i] == 'D'){
						i++;
						SLevantarD.rotacao = Conversor.getInt(mensagem,i);
					}else if(mensagem[i] == 'E'){
						i++;
						SLevantarE.rotacao = Conversor.getInt(mensagem,i);
					}
					envioBuffer[0] = (byte)'a';
					envioBuffer[1] = (byte)'c';  // mensagem a ser enviada ao cliente
					envioBuffer[2] = (byte)'k';
					envioBuffer[3] = 10;
					cliente.Send(envioBuffer); //envia mensagem ao cliente
				break;
				case 'P':
					i++;
					if(mensagem[i] == 'V'){
						i++;
						SPescoco1.rotacao = Conversor.getInt(mensagem,i);
					}else if(mensagem[i] == 'H'){
						i++;
						SPescoco2.rotacao = Conversor.getInt(mensagem,i);
					}
					envioBuffer[0] = (byte)'a';
					envioBuffer[1] = (byte)'c';  // mensagem a ser enviada ao cliente
					envioBuffer[2] = (byte)'k';
					envioBuffer[3] = 10;
					cliente.Send(envioBuffer); //envia mensagem ao cliente
				break;
				case 'M':
				    i++;
				    if(mensagem[i] == 'D'){
						i++;
						SBracoD.rotacao = Conversor.getInt(mensagem,i);
					}else if(mensagem[i] == 'E'){
						i++;
						SBracoE.rotacao = Conversor.getInt(mensagem,i);
					}
					envioBuffer[0] = (byte)'a';
					envioBuffer[1] = (byte)'c';  // mensagem a ser enviada ao cliente
					envioBuffer[2] = (byte)'k';
					envioBuffer[3] = 10;
					cliente.Send(envioBuffer); //envia mensagem ao cliente
				break;
			}
		}
	}
}
