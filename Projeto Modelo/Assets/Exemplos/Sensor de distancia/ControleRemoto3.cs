/*--------------------------------
 * ControleRemoto3.cs 18/10/2014
 * Definicao: Script utilitario responsavel
 * pelo controle do ambiente da Cena03, esse
 * controle consiste em movimentar um objeto
 * dada a entrada do teclado, assim como configurar
 * a instancia do prefab SensorIV presente no ambiente
 * Uso: Usado para teste da Cena03.
 *-------------------------------- 
*/
using UnityEngine;
using System.Collections;

public class ControleRemoto3 : MonoBehaviour {
	private Sensor_Infra_Vermelho sensor; // variavel que guardara a referencia para o sensorIV
	private Transform obstaculo; // obstaculo o qual sera movimentado estando presente no ambiente
	private bool loc;   // variavel auxiliar para controle do uso das teclas do teclado

	//Inicializaçao do controlador
	void Start () {
		sensor = GameObject.Find("SensorIV").GetComponent<Sensor_Infra_Vermelho>(); //atribui o sensor a variavel
		obstaculo = GameObject.Find("Obstaculo").transform; //atribui o objeto a variavel
	}

	//funcao responsavel por dispor as caixas de dialogo na tela do usuario
	void OnGUI()
	{
		Rect caixa = new Rect(5.0f, 24f, (float)240f, 24f);
		GUI.Box(caixa, "esquerda, direita: Mover Obstaculo");
		caixa = new Rect(5.0f, 24f+25f, (float)160f, 24f);
		GUI.Box(caixa, "cima, baixo : Ruido");
		caixa = new Rect(5.0f, 24f+25f*2, (float)120f, 24f);
		GUI.Box(caixa, "A : Mostra Raio");
		caixa = new Rect(5.0f, 24f+25f*3, (float)120f, 24f);
		GUI.Box(caixa, "Leitura: "+sensor.ultima_leitura.ToString());
	}

	// funcao de atualizacao responsavel por toda a logica de controle
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)&& !loc)
		{sensor.ruido += 1;}
		if(Input.GetKey(KeyCode.DownArrow) && !loc) //controla o ruido do sensor dado entrada
		{sensor.ruido -= 1;}
		if(Input.GetKey(KeyCode.LeftArrow))
		{obstaculo.position = obstaculo.position + Vector3.forward*0.001f;}
		if(Input.GetKey(KeyCode.RightArrow))
		{obstaculo.position = obstaculo.position - Vector3.forward*0.001f;} // posiciona o obstaculo dado entrada
		if(Input.GetKey(KeyCode.A) && !loc)
		{	sensor.ver_raio = !sensor.ver_raio;		//liga/desliga a plotagem do raio por parte do sensor dado entrada
		}
		sensor.GetDistancia ();
		loc = Input.anyKey;
	}
}
