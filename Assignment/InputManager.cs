


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public enum MouseButton { Left, Right, Middle, X1, X2 };

public class InputManager
{
    public KeyboardState currentKeyState { get; private set; }
    public KeyboardState prevKeyState { get; private set; }
    MouseState currentMouseState, prevMouseState;
    private static InputManager instance;


    //TODO: Make this work for multiple controllers
    public GamePadState currentGamepadState { get; private set; }
    public GamePadState previousGamepadState { get; private set; }
    public GamePadButtons currentButtonState { get; private set; }
    public GamePadButtons previousButtonState { get; private set; }



    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }
    }


    public void Update()
    {
        prevKeyState = currentKeyState;
        prevMouseState = currentMouseState;
        previousButtonState = currentButtonState;
        previousGamepadState = currentGamepadState;

        // if (!ScreenManager.Instance.IsTransitioning)
        //{
        currentKeyState = Keyboard.GetState();
        currentMouseState = Mouse.GetState();

        currentGamepadState = GamePad.GetState(PlayerIndex.One);
        currentButtonState = currentGamepadState.Buttons;
        
        // }
    }

    public Vector2 GetMousePosition()
    {
        return new Vector2(currentMouseState.X, currentMouseState.Y);
    }
    public int GetMousePositionX()
    {
        return currentMouseState.X;
    }
    public int GetMousePositionY()
    {
        return currentMouseState.Y;
    }

    public bool KeyPressed(params Keys[] keys)
    {
        foreach (Keys key in keys)
        {
            if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
        }
        return false;

    }

    public bool KeyReleased(params Keys[] keys)
    {
        foreach (Keys key in keys)
        {
            if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
        }
        return false;
    }

    public bool KeyDown(params Keys[] keys)
    {
        foreach (Keys key in keys)
        {
            if (currentKeyState.IsKeyDown(key))
                return true;
        }
        return false;
    }

    public bool KeyUp(params Keys[] keys)
    {
        foreach (Keys key in keys)
        {
            if (currentKeyState.IsKeyUp(key))
                return true;
        }
        return false;
    }


    public bool ButtonUp(params Buttons[] buttons)
    {
        foreach (Buttons button in buttons)
        {
            if (currentGamepadState.IsButtonUp(button))
                return true;
        }
        return false;
    }
    public bool ButtonDown(params Buttons[] buttons)
    {
        foreach (Buttons button in buttons)
        {
            if (currentGamepadState.IsButtonDown(button))
                return true;
        }
        return false;
    }
    public bool ButtonPressed(params Buttons[] buttons)
    {
        foreach (Buttons button in buttons)
        {
            if (currentGamepadState.IsButtonDown(button) && previousGamepadState.IsButtonUp(button))
                return true;
        }
        return false;

    }

    public bool ButtonReleased(params Buttons[] buttons)
    {
        foreach (Buttons button in buttons)
        {
            if (currentGamepadState.IsButtonUp(button) && previousGamepadState.IsButtonDown(button))
                return true;
        }
        return false;
    }







    public bool MousePressed(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.Right:
                if (currentMouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.Middle:
                if (currentMouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.X1:
                if (currentMouseState.XButton1 == ButtonState.Pressed && prevMouseState.XButton1 == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.X2:
                if (currentMouseState.XButton2 == ButtonState.Pressed && prevMouseState.XButton2 == ButtonState.Released)
                    return true;
                else
                    return false;
            default:
                return false;

        }
    }

    public bool MouseReleased(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                if (currentMouseState.LeftButton == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.Right:
                if (currentMouseState.RightButton == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.Middle:
                if (currentMouseState.MiddleButton == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.X1:
                if (currentMouseState.XButton1 == ButtonState.Released)
                    return true;
                else
                    return false;
            case MouseButton.X2:
                if (currentMouseState.XButton2 == ButtonState.Released)
                    return true;
                else
                    return false;
            default:
                return false;

        }
    }

    public bool MouseDown(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                    return true;
                else
                    return false;
            case MouseButton.Right:
                if (currentMouseState.RightButton == ButtonState.Pressed)
                    return true;
                else
                    return false;
            case MouseButton.Middle:
                if (currentMouseState.MiddleButton == ButtonState.Pressed)
                    return true;
                else
                    return false;
            case MouseButton.X1:
                if (currentMouseState.XButton1 == ButtonState.Pressed)
                    return true;
                else
                    return false;
            case MouseButton.X2:
                if (currentMouseState.XButton2 == ButtonState.Pressed)
                    return true;
                else
                    return false;
            default:
                return false;

        }
    }


}

