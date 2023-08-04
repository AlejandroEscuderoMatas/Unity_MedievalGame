using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScenes
{
    public static readonly String MENU_SCENE     = "MENU_SCENE";
    public static readonly String LOADING_SCENE  = "LOADING_SCENE";
    public static readonly String TUTORIAL_SCENE = "TUTORIAL_SCENE";
    public static readonly String GAME_SCENE     = "GAME_SCENE";
}

public class AppPlayerPrefKeys
{
    public static readonly string MUSIC_VOLUME   = "MusicVolume";
    public static readonly string SFX_VOLUME     = "SfxVolume";
}

public class AppPaths
{
    public static readonly String PERSISTENT_DATA     = Application.persistentDataPath;
    public static readonly String PATH_RESOURCE_SFX   = "Music/MenuSfx/";
    public static readonly String PATH_RESOURCE_MUSIC = "Music/MenuBackground/";
}

public class AppSounds
{
    public static readonly string	MAIN_TITLE_MUSIC = "MainTitle";
    public static readonly string	GAME_MUSIC       = "backMusicMenus";
    public static readonly string	BUTTON_SFX       = "button_sound";
}
