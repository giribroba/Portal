using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Camera portalCam;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Portal outroPortal;

    private GameObject seguidor; //Serve para mostrar a posição do player
    private RenderTexture imgOutro;

    void Awake()
    {
        seguidor = this.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        Seguidor();
        PosicionarCamera();
        ProjetarImagem();
    }

    private void Seguidor()
    {
        seguidor.transform.position = playerCam.transform.parent.position;
    }

    private void PosicionarCamera()
    {
        seguidor.transform.forward = this.transform.localPosition - playerCam.transform.parent.position;
        seguidor.transform.eulerAngles -= Vector3.right * seguidor.transform.eulerAngles.x;
        outroPortal.portalCam.transform.eulerAngles = seguidor.transform.eulerAngles + (Vector3.up * (this.transform.eulerAngles.y - outroPortal.transform.eulerAngles.y));
    }

    private void ProjetarImagem()
    {
        imgOutro = new RenderTexture(Screen.width, Screen.height, 0);
        portalCam.targetTexture = imgOutro;
        outroPortal.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex",imgOutro);
    }
}
