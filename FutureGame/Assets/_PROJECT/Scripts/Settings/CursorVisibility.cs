using UnityEngine;

public static class CursorVisibility
{

    private static int openTabs = 0;

    public static void SetCursorVisible()
    {

        if (openTabs == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        openTabs++;
    }

    public static void SetCursorHidden()
    {

        if (openTabs > 0)
        {
            openTabs--;
        }

        if (openTabs == 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}


