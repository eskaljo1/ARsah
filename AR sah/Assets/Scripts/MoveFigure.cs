using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFigure : MonoBehaviour
{
    private GameObject touchedObject;
    private GameObject figure;
    private bool figureIsTouched = false;
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
                            WhiteFigure.attacking = true;
                            BlackFigure.attacking = false;
                            figure = touchedObject;
                            figureIsTouched = true;
                            previousMat = figure.GetComponent<MeshRenderer>().material;
                            figure.GetComponent<MeshRenderer>().material = selectedMat;
                            figure.GetComponent<Animator>().SetTrigger("Click");
                        }
                        else
                        {
                            figure.GetComponent<MeshRenderer>().material = previousMat;
                            figure.transform.position = touchedObject.transform.position;
                            figureIsTouched = false;
                        }
                    }
                    else
                    {
                        if (!figureIsTouched)
                        {
                            WhiteFigure.attacking = false;
                            BlackFigure.attacking = true;
                            figure = touchedObject;
                            figureIsTouched = true;
                            previousMat = figure.GetComponent<MeshRenderer>().material;
                            figure.GetComponent<MeshRenderer>().material = selectedMat;
                            figure.GetComponent<Animator>().SetTrigger("Click");
                        }
                        else
                        {
                            figure.GetComponent<MeshRenderer>().material = previousMat;
                            figure.transform.position = touchedObject.transform.position;
                            figureIsTouched = false;
                        }
                    }
                }
                else if ((touchedObject.tag == "WhiteEnd" || touchedObject.tag == "BlackEnd" || touchedObject.tag == "Board") && figureIsTouched)
                {
                    figure.GetComponent<MeshRenderer>().material = previousMat;
                    figure.transform.position = touchedObject.transform.position;
                    figureIsTouched = false;
                }
            }
        }
    }
}
