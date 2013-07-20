using UnityEngine;

[RequireComponent(typeof(NetworkView))]
public class PlayerScript : MonoBehaviour
{
    public void Awake()
    {
        if (!networkView.isMine)
        {
            // Если не мы владельцы данного объекта, то выключаем данный скрипт.
            // Но помним, что RPC и OnSerializeNetworkView работают в любом случае
            enabled = false;
        }
    }

    public void Update()
    {

        if (networkView.isMine)
        {
            // Только владелец может двигать куб!          
            Vector3 moveDirection = new Vector3(
               Input.GetAxis("Horizontal"),0 , Input.GetAxis("Vertical"));
		//		Input.GetKey(KeyCode.Q)?1:0,Input.GetKey(KeyCode.W)?1:0,Input.GetKey(KeyCode.E)?1:0);
			
            float speed = 5;
            transform.Translate(speed * moveDirection * Time.deltaTime);
        }

    }


    public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            // Выполняется у владельца networkview; 
            // Сервер рассылает позицию по сети

            Vector3 pos = transform.position;
            stream.Serialize(ref pos);//"Кодирование" и рассылка

        }
        else
        {
            // Выполняется у всех остальных; 
            // Клиенты получают позицию и устанавливают ее

            Vector3 posReceive = Vector3.zero;
            stream.Serialize(ref posReceive); //"Декодирование" и прием
            transform.position = posReceive;

        }
    }
}