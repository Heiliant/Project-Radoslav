using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChangeControls : MonoBehaviour {

	public void _Back()
    {
        gameObject.SetActive(false);
    }

    public void _ChangeKey(int b)
    {
        KeyCode a;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            a = KeyCode.Q;
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            a = KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            a = KeyCode.E;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            a = KeyCode.R;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            a = KeyCode.T;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            a = KeyCode.Y;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            a = KeyCode.U;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            a = KeyCode.I;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            a = KeyCode.O;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            a = KeyCode.P;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            a = KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            a = KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            a = KeyCode.D;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            a = KeyCode.F;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            a = KeyCode.G;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            a = KeyCode.H;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            a = KeyCode.J;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            a = KeyCode.K;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            a = KeyCode.L;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            a = KeyCode.Z;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            a = KeyCode.X;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            a = KeyCode.C;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            a = KeyCode.V;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            a = KeyCode.B;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            a = KeyCode.N;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            a = KeyCode.M;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            a = KeyCode.Space;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            a = KeyCode.Alpha0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            a = KeyCode.Alpha1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            a = KeyCode.Alpha2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            a = KeyCode.Alpha3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            a = KeyCode.Alpha4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            a = KeyCode.Alpha5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            a = KeyCode.Alpha6;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            a = KeyCode.Alpha7;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            a = KeyCode.Alpha8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            a = KeyCode.Alpha9;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            a = KeyCode.UpArrow;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            a = KeyCode.DownArrow;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            a = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            a = KeyCode.RightArrow;
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            a = KeyCode.LeftAlt;
        }
        else if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            a = KeyCode.RightAlt;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            a = KeyCode.LeftShift;
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            a = KeyCode.RightShift;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            a = KeyCode.LeftControl;
        }
        else if (Input.GetKeyDown(KeyCode.RightControl))
        {
            a = KeyCode.RightControl;
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            a = KeyCode.Tab;
        }
        else if (Input.GetKeyDown(KeyCode.Comma))
        {
            a = KeyCode.Comma;
        }
        else if (Input.GetKeyDown(KeyCode.Colon))
        {
            a = KeyCode.Colon;
        }
        else if (Input.GetKeyDown(KeyCode.Less))
        {
            a = KeyCode.Less;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            a = KeyCode.Return;
        }
        else
        {
            a = KeyCode.None;
        }

        if (a != KeyCode.None)
        {
            switch (b) {
                case 0:
                    FindObjectOfType<CambioFormas>().setTransfKey(a);
                    break;
                case 1:
                    FindObjectOfType<CambioFormas>().setEscKey(a);
                    break;
                case 2:
                    FindObjectOfType<PlayerControl>().setFistKey(a);
                    break;
                case 3:
                    FindObjectOfType<PlayerControl>().setRightKey(a);
                    break;
                case 4:
                    FindObjectOfType<PlayerControl>().setLeftKey(a);
                    break;
                case 5:
                    FindObjectOfType<PlayerControl>().setJumpKey(a);
                    break;

            }
        }
    }

}
