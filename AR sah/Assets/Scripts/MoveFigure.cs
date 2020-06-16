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
                            previousMat = figure.GetComponentInChildren<MeshRenderer>().material;
                            figure.GetComponentInChildren<MeshRenderer>().material = selectedMat;
                            figure.GetComponent<Animator>().SetTrigger("Click");
                            figure.GetComponent<AudioSource>().Play();
                        }
                        else if (figure.tag == "White")
                        {
                            figure.GetComponentInChildren<MeshRenderer>().material = previousMat;
                            figure.GetComponent<Animator>().SetTrigger("Move");
                            figure = touchedObject;
                            previousMat = figure.GetComponentInChildren<MeshRenderer>().material;
                            figure.GetComponentInChildren<MeshRenderer>().material = selectedMat;
                            figure.GetComponent<Animator>().SetTrigger("Click");
                            figure.GetComponent<AudioSource>().Play();
                        }
                        else
                        {
                            figure.GetComponentInChildren<MeshRenderer>().material = previousMat;
                            figure.transform.position = touchedObject.transform.position;
                            figure.GetComponent<Animator>().SetTrigger("Eat");
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
                            previousMat = figure.GetComponentInChildren<MeshRenderer>().material;
                            figure.GetComponentInChildren<MeshRenderer>().material = selectedMat;
                            figure.GetComponent<Animator>().SetTrigger("Click");
                            figure.GetComponent<AudioSource>().Play();
                        }
                        else if (figure.tag == "Black")
                        {
                            figure.GetComponentInChildren<MeshRenderer>().material = previousMat;
                            figure.GetComponent<Animator>().SetTrigger("Move");
                            figure = touchedObject;
                            previousMat = figure.GetComponentInChildren<MeshRenderer>().material;
                            figure.GetComponentInChildren<MeshRenderer>().material = selectedMat;
                            figure.GetComponent<Animator>().SetTrigger("Click");
                            figure.GetComponent<AudioSource>().Play();
                        }
                        else
                        {
                            figure.GetComponentInChildren<MeshRenderer>().material = previousMat;
                            figure.transform.position = touchedObject.transform.position;
                            figure.GetComponent<Animator>().SetTrigger("Eat");
                            figureIsTouched = false;
                        }
                    }
                }
                else if ((touchedObject.tag == "WhiteEnd" || touchedObject.tag == "BlackEnd" || touchedObject.tag == "Board") && figureIsTouched)
                {
                    figure.GetComponentInChildren<MeshRenderer>().material = previousMat;
                    figure.transform.position = touchedObject.transform.position;
                    figure.GetComponent<Animator>().SetTrigger("Move");
                    figureIsTouched = false;
                }
            }
        }
    }
}
