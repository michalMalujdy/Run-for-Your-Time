using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomButtons
{
    public class Button
    {
        bool clickedDown = false;
        bool clicked = false;
        int ID;
        public bool IsButtonActive { get; set; }

        public float LeftEdge { get; private set; }
        public float RightEdge { get; private set; }
        public float DownEdge { get; private set; }
        public float UpEdge { get; private set; }

        public HandlePlayerMovement HandlePlayerMovement { get; private set; }

        public Button(float leftEdge, float rightEdge, float downEdge, float upEdge)
        {
            LeftEdge = leftEdge * Screen.width;
            RightEdge = rightEdge * Screen.width;
            DownEdge = downEdge * Screen.height;
            UpEdge = upEdge * Screen.height;

            HandlePlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<HandlePlayerMovement>();
            IsButtonActive = true;
        }

        public bool IsButtonDown()
        {
            if (IsButtonActive)
            {
                int fingersRunning = 0;

                foreach (Touch touch in Input.touches)
                {
                    if (touch.position.x <= RightEdge && touch.position.x >= LeftEdge &&
                        touch.position.y >= DownEdge && touch.position.y <= UpEdge)
                    {
                        fingersRunning++;
                    }
                }
                if (fingersRunning > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        public bool IsButtonClicked()
        {

            if (IsButtonActive)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (!clickedDown && touch.phase == TouchPhase.Began &&
                       touch.position.x <= RightEdge && touch.position.x >= LeftEdge &&
                       touch.position.y >= DownEdge && touch.position.y <= UpEdge)
                    {
                        clickedDown = true;
                        ID = touch.fingerId;
                    }

                    if (clickedDown && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && touch.fingerId == ID &&
                       touch.position.x <= RightEdge && touch.position.x >= LeftEdge &&
                       touch.position.y >= DownEdge && touch.position.y <= UpEdge)
                    {
                        clicked = true;
                        clickedDown = false;
                    }
                    else {
                        clicked = false;
                    }
                }
                return clicked;
            }
            else {
                clicked = false;
                clickedDown = false;
                return false;
            }
        }
    }
}
