/*--------------------------------
 * Movimentacao_mouse.cs 26/10/2014
 * Definicao: Script utilitario para posicionamento
 * e movimentacao de objetos, usando o mouse.
 * Uso: Usado geralmente em cameras.
 *-------------------------------- 
*/

/*entradas padrao:
	shift direito: habilita/desabilita translaçao
	botao direito do mouse (segurar): habilita rotacao do mouse e desabilita translacao
	movimento do mouse: movimenta/rotaciona objeto
	mousewheel: translarda para cima e para baixo
*/

using UnityEngine;
using System.Collections;
public class Movimentacao_mouse : MonoBehaviour {
	
	public float velocidade = 0.002f; // regula a velocidade de deslocamento da camera em metros por ciclo
	public float sensibilidade = 15f; // regula a sensibilidade do mouse
	private bool ativo; // indica se o usuario esta no modo movimentacao ou nao
	private bool[] loc; // variavel auxiliar

	private float rotationY = 0.0f;

	void Start () {
		ativo = false;
		loc = new bool[10];
	}
	
	// funcao responsavel por movimentar o objeto dado entradas
	void Translacao () {
		//Os movimentos sao baseados na varivavel 'velocidade' em metros nas direcoes:	
		this.transform.position = this.transform.position + this.transform.right * Input.GetAxis("Mouse X") * sensibilidade * velocidade; // esquerda/direita
		this.transform.position = this.transform.position + this.transform.up * Input.GetAxis("Mouse Y") * sensibilidade * velocidade; // cima/baixo
		this.transform.position = this.transform.position + this.transform.forward * Input.GetAxis("Mouse ScrollWheel") * 2 *sensibilidade * velocidade; //frente/atraz
	}
	
	// funcao responsavel por rotacionar o objeto dado entradas
	void Rotacao(){
		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibilidade;
	    rotationY += Input.GetAxis("Mouse Y") * sensibilidade;
		rotationY = Mathf.Clamp (rotationY, -70F, 70F);
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
	
	//funcao de atualizacao que verifica se esta no modo movimentacao e dai chama as funcoes de rotacao
	//e translacao
	void Update () {
		if (Input.GetKey (KeyCode.RightShift)) {
			if(!loc[0])
			{
				ativo = !ativo;				//liga/desliga o modo translacao
			}
			loc[0] = true;
		}else {loc[0] = false;}

		Screen.lockCursor = false; //destrava mouse

		if(Input.GetKey (KeyCode.Mouse1)){ // a rotacao e ativada pelo botao direito do mouse
			Screen.lockCursor = true;
			Rotacao ();
		}else if (ativo) {
			Screen.lockCursor = true;  //trava o mouse para que o movimento do mesmo nao atrapalhe o trabalho
			Translacao ();
		}
	}
}
