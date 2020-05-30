using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFigure : MonoBehaviour
{
    private GameObject touchedObject;
    private GameObject figure;
    private bool figureIsTouched = false;
    private bool white = false;
    private Material previousMat;
    public Material selectedMat;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                touchedObject = hit.transform.gameObject;
                if (touchedObject.tag == "White" || touchedObject.tag == "Black")
                {
                    if (touchedObject.tag == "White")
                    {
                        if (!figureIsTouched)
                        {
                            white = true;
                            WhiteFigure.attacking = true;
                            BlackFigure.attacking = false;
                            figure = touchedObject;
                            figureIsTouched = true;
                            previousMat = figure.GetComponent<MeshRenderer>().material;
                            selectedMat.mainTexture = previousMat.mainTexture;
                            figure.GetComponent<MeshRenderer>().material = selectedMat;
                        }
                        else
                        {
                            figure.GetComponent<MeshRenderer>().material = previousMat;
                            figure.transform.position = new Vector3(touchedObject.transform.position.x, touchedObject.transform.position.y, figure.transform.position.z);
                            figure = null;
                            figureIsTouched = false;
                        }
                    }
                    else
                    {
                        if (!figureIsTouched)
                        {
                            white = false;
                            WhiteFigure.attacking = false;
                            BlackFigure.attacking = true;
                            figure = touchedObject;
                            figureIsTouched = true;
                            previousMat = figure.GetComponent<MeshRenderer>().material;
                            selectedMat.mainTexture = previousMat.mainTexture;
                            figure.GetComponent<MeshRenderer>().material = selectedMat;
                        }
                        else
                        {
                            figure.GetComponent<MeshRenderer>().material = previousMat;
                            figure.transform.position = new Vector3(touchedObject.transform.position.x, touchedObject.transform.position.y, figure.transform.position.z);
                            figure = null;
                            figureIsTouched = false;
                        }
                    }
                }
                else if (touchedObject.tag == "Board" && figureIsTouched)
                {
                    figure.GetComponent<MeshRenderer>().material = previousMat;
                    figure.transform.position = new Vector3(touchedObject.transform.position.x, touchedObject.transform.position.y, figure.transform.position.z);
                    figure = null;
                    figureIsTouched = false;
                }
            }
        }
    }
}
