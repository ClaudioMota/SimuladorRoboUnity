/*--------------------------------
 * ControleRemoto.cs 18/10/2014
 * Definicao: Script utilitario responsavel
 * pelo controle do ambiente da Cena12, esse
 * controle consiste em direcionar motores 
 * do braço robotico presente no ambiente.
 * Uso: Usado para teste da Cena12.
 *-------------------------------- 
*/

using UnityEngine;
using System.Collections;

public class ControleRemoto12 : MonoBehaviour {
	private Servo_Motor_Limitado servo1;  //variaveis que guardarao as referencias dos objetos relativos a
										  //cada servo motor limitado do manipulador robotico
	private Servo_Motor_Limitado servo2;
	private Servo_Motor_Limitado servo3;
	private Servo_Motor_Limitado servo4;
	private Servo_Motor_Limitado servo5;
	private bool loc; // variavel auxiliar para previnir duplo clique ao pressionar determinados botoes do teclado
	// Use this for initialization
	void Start () {
		servo1 = GameObject.Find("Servo1").GetComponent<Servo_Motor_Limitado>(); //busca o objeto referente ao servo motor 
																			     //limitado no ambiente e referencia o mesmo com a variavel
		servo2 = GameObject.Find("Servo2").GetComponent<Servo_Motor_Limitado>();
		servo3 = GameObject.Find("Servo5").GetComponent<Servo_Motor_Limitado>();
		servo4 = GameObject.Find("Servo4").GetComponent<Servo_Motor_Limitado>();
		servo5 = GameObject.Find("Servo3").GetComponent<Servo_Motor_Limitado>();
	}

	//funcao responsavel por dispor as caixas de dialogo na tela do usuario
	void OnGUI()
	{
		Rect caixa = new Rect(5.0f, 24f, (float)160f, 24f);
		GUI.Box(caixa, "esquerda,direita: Servo1");
		caixa = new Rect(5.0f, 24f+25f, (float)160f, 24f);
		GUI.Box(caixa, "cima,baixo : Servo2");
		caixa = new Rect(5.0f, 24f+25f*2, (float)120f, 24f);
		GUI.Box(caixa, "A,D : Servo3,4");
	}

	// funcao de atualizacao responsavel por toda a logica de controle
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow))
		{servo2.rotacao-=1;}
		if(Input.GetKey(KeyCode.DownArrow)) //essas linhas de codigo regualam o direcionamento dos sevomotores dado entrada
		{servo2.rotacao+=1;}
		if(Input.GetKey(KeyCode.LeftArrow))
		{servo1.rotacao-=1;}
		if(Input.GetKey(KeyCode.RightArrow))
		{servo1.rotacao+=1;}
		if(Input.GetKey(KeyCode.A))
		{	servo3.rotacao-=1;
		 	servo4.rotacao+=1;
		}
		if(Input.GetKey(KeyCode.D))
		{
			servo3.rotacao+=1;
			servo4.rotacao-=1;
		}
		if(Input.GetKey(KeyCode.W))
		{	servo5.rotacao-=1;
		}
		if(Input.GetKey(KeyCode.S))
		{	servo5.rotacao+=1;
		}
		loc = Input.anyKey;
	}
}
