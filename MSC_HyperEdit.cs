using System;
using HutongGames.PlayMaker;
using MSCLoader;
using UnityEngine;

namespace MSC_HyperEdit
{
    public class MSC_HyperEdit : Mod
    {
        //Mod info
        public override string ID => "MSC_HyperEdit";
        public override string Name => "MSC_HyperEdit";
        public override string Author => "Sylvrwynd";
        public override string Version => "0.0.1";

        //Bool
        private bool _guiHyperEditShow;
        private bool _guiSatsumaPartShow;
        private bool _guiSatsumaPartEditShow;
        private bool _guiSatsumaFluidShow;
        private bool _guiSatsumaFluidEditShow;
        private bool _guiGifuShow;
        private bool _guiGifuEditShow;
        private bool _guiMopedShow;
        private bool _guiMopedEditShow;
        private bool _guiVanShow;
        private bool _guiVanEditShow;
        private bool _guiTractorShow;
        private bool _guiTractorEditShow;

        private bool _getSatsumaPartValues;
        private bool _getSatsumaFluidValues;
        private bool _getGifuValues;
        private bool _getMopedValues;
        private bool _getVanValues;
        private bool _getTractorValues;

        private bool _mouseFree;

        //GUIBOX
        private Rect _guiHyperEditBox = new Rect(0f, 0f, 150f, 195f);
        private Rect _guiSatsumaPartBox = new Rect(200f, 0f, 190f, 240f);
        private Rect _guiSatsumaPartEditBox = new Rect(200f, 0f, 350f, 265f);
        private Rect _guiSatsumaFluidBox = new Rect(Screen.width - 190f, 0f, 190f, 140f);
        private Rect _guiSatsumaFluidEditBox = new Rect(Screen.width - 350f, 0f, 350f, 165f);
        private Rect _guiGifuBox = new Rect(Screen.width - 190f, 0f, 190f, 85f);
        private Rect _guiGifuEditBox = new Rect(Screen.width - 350f, 0f, 350f, 85f);
        private Rect _guiMopedBox = new Rect(Screen.width - 190f, 0f, 190f, 45f);
        private Rect _guiMopedEditBox = new Rect(Screen.width - 350f, 0f, 350f, 45f);
        private Rect _guiVanBox = new Rect(Screen.width - 190f, 0f, 190f, 45f);
        private Rect _guiVanEditBox = new Rect(Screen.width - 350f, 0f, 350f, 45f);
        private Rect _guiTractorBox = new Rect(Screen.width - 190f, 0f, 190f, 45f);
        private Rect _guiTractorEditBox = new Rect(Screen.width - 350f, 0f, 350f, 45f);

        // Keybinds
        private readonly Keybind _showHyperEditGui = new Keybind("HyperEditGuiKey", "HyperEdit GUI", KeyCode.G, KeyCode.LeftAlt);
        private readonly Keybind _releaseMouse = new Keybind("ReleaseMouseKey", "Release Mouse", KeyCode.M, KeyCode.RightAlt);

        //Satsuma floats
        private FsmFloat _wearAlternator;
        private FsmFloat _wearCrankshaft;
        private FsmFloat _wearGearbox;
        private FsmFloat _wearHeadgasket;
        private FsmFloat _wearPiston1;
        private FsmFloat _wearPiston2;
        private FsmFloat _wearPiston3;
        private FsmFloat _wearPiston4;
        private FsmFloat _wearRockershaft;
        private FsmFloat _wearStarter;
        private FsmFloat _wearWaterpump;
        private FsmFloat _oilLevel;
        private FsmFloat _wearFanbelt;
        private FsmFloat _satsumaFuel;
        private FsmFloat _regularCoolant;
        private FsmFloat _racingCoolant;
        private FsmFloat _brakeFluidF;
        private FsmFloat _brakeFluidR;
        private FsmFloat _clutchFluid;
        // Gifu floats
        private FsmFloat _gifuFuel;
        private FsmFloat _gifuFuelOil;
        private FsmFloat _gifuWaste;
        // Moped floats
        private FsmFloat _mopedFuel;
        // Van floats
        private FsmFloat _vanFuel;
        // Tractor floats
        private FsmFloat _tractorFuel;

        //Called when mod is loading
        public override void OnLoad()
        {
            Keybind.Add(this, _showHyperEditGui);
            Keybind.Add(this, _releaseMouse);
        }

        // Update is called once per frame
        public override void FixedUpdate()
        {
            //Unload Mod
            UnloadMod();

            //Toggle GUI
            GuiToggler();

            //IF GUI is open update
            NewGameLoader();

            // Mouse Release
            MouseToggle();
        }

        private void UnloadMod()
        {
            if (Application.loadedLevel != 3)
            {
                _getSatsumaPartValues = false;
                _getSatsumaFluidValues = false;
                _getGifuValues = false;
                _getMopedValues = false;
                _getVanValues = false;
                _getTractorValues = false;
                // Release mouse to default application control
                _mouseFree = false;
                FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
            }
        }

        private void GuiToggler()
        {
            if (_showHyperEditGui.IsDown())
            {
                _guiHyperEditShow = !_guiHyperEditShow;
            }
        }

        private void MouseToggle()
        {
            if (_releaseMouse.IsDown())
            {
                if (!_mouseFree)
                {
                    FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = true;
                    _mouseFree = true;
                }
                else
                {
                    FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu").Value = false;
                    _mouseFree = false;
                }
            }
        }


        private void NewGameLoader()
        {
            if (Application.loadedLevel == 3 && _guiSatsumaPartShow)
            {
                GetSatsumaPartStatus();
            }
            if (Application.loadedLevel == 3 && _guiSatsumaPartEditShow)
            {
                GetSatsumaPartStatus();
            }
            if (Application.loadedLevel == 3 && _guiSatsumaFluidShow)
            {
                GetSatsumaFluidStatus();
            }
            if (Application.loadedLevel == 3 && _guiSatsumaFluidEditShow)
            {
                GetSatsumaFluidStatus();
            }
            if (Application.loadedLevel == 3 && _guiGifuShow)
            {
                GetGifuStatus();
            }
            if (Application.loadedLevel == 3 && _guiGifuEditShow)
            {
                GetGifuStatus();
            }
            if (Application.loadedLevel == 3 && _guiMopedShow)
            {
                GetGifuStatus();
            }
            if (Application.loadedLevel == 3 && _guiMopedEditShow)
            {
                GetGifuStatus();
            }
            if (Application.loadedLevel == 3 && _guiVanShow)
            {
                GetVanStatus();
            }
            if (Application.loadedLevel == 3 && _guiVanEditShow)
            {
                GetVanStatus();
            }
            if (Application.loadedLevel == 3 && _guiTractorShow)
            {
                GetTractorStatus();
            }
            if (Application.loadedLevel == 3 && _guiTractorEditShow)
            {
                GetTractorStatus();
            }
        }

        //Called when mod is loading
        public override void OnGUI()
        {
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiHyperEditShow)
            {
                _guiHyperEditBox = GUI.Window(620851, _guiHyperEditBox, HyperEditLayout,"<color=white><b>HyperEdit</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiSatsumaPartShow)
            {
                _guiSatsumaPartBox = GUI.Window(620852, _guiSatsumaPartBox, SatsumaPartLayout, "<color=white><b>Satsuma Engine Status</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiSatsumaPartEditShow)
            {
                _guiSatsumaPartEditBox = GUI.Window(620853, _guiSatsumaPartEditBox, SatsumaPartEditLayout, "<color=white><b>Satsuma Engine Status</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiSatsumaFluidShow)
            {
                _guiSatsumaFluidBox = GUI.Window(620854, _guiSatsumaFluidBox, SatsumaFluidLayout, "<color=white><b>Satsuma Fluid Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiSatsumaFluidEditShow)
            {
                _guiSatsumaFluidEditBox = GUI.Window(620855, _guiSatsumaFluidEditBox, SatsumaFluidEditLayout, "<color=white><b>Satsuma Fluid Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiGifuShow)
            {
                _guiGifuBox = GUI.Window(620856, _guiGifuBox, GifuLayout, "<color=white><b>Gifu Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiGifuEditShow)
            {
                _guiGifuEditBox = GUI.Window(620857, _guiGifuEditBox, GifuEditLayout, "<color=white><b>Gifu Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiMopedShow)
            {
                _guiMopedBox = GUI.Window(620858, _guiMopedBox, MopedLayout, "<color=white><b>Moped Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiMopedEditShow)
            {
                _guiMopedEditBox = GUI.Window(620859, _guiMopedEditBox, MopedEditLayout, "<color=white><b>Moped Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiVanShow)
            {
                _guiVanBox = GUI.Window(6208510, _guiVanBox, VanLayout, "<color=white><b>Van Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiVanEditShow)
            {
                _guiVanEditBox = GUI.Window(6208511, _guiVanEditBox, VanEditLayout, "<color=white><b>Van Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiTractorShow)
            {
                _guiTractorBox = GUI.Window(6208512, _guiTractorBox, TractorLayout, "<color=white><b>Tractor Levels</b></color>");
            }
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            if (Application.loadedLevel == 3 && _guiTractorEditShow)
            {
                _guiTractorEditBox = GUI.Window(6208513, _guiTractorEditBox, TractorEditLayout, "<color=white><b>Tractor Levels</b></color>");
            }
        }

        private void HyperEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle( out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            HyperEditGUI( buttonStyle, labelStyle, errorStyle);
        }
        private void SatsumaPartLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            SatsumaPartGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void SatsumaPartEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            SatsumaPartEditGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void SatsumaFluidLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            SatsumaFluidGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void SatsumaFluidEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            SatsumaFluidEditGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void GifuLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            GifuGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void GifuEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            GifuEditGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void MopedLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            MopedGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void MopedEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            MopedEditGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void VanLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            VanGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void VanEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            VanEditGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void TractorLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            TractorGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }
        private void TractorEditLayout(int id)
        {
            //GuiStyle List
            GuiStyle(out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle);
            //Call Window with styles
            TractorEditGUI(buttonStyle, errorStyle, labelStyle, textStyle);
        }

        /******************************* GUI Styles Functions *************************************/
        private static void GuiStyle( out GUIStyle buttonStyle, out GUIStyle labelStyle, out GUIStyle errorStyle, out GUIStyle textStyle)
        {
            buttonStyle = new GUIStyle(GUI.skin.GetStyle("Button"))
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 11
            };
            buttonStyle.normal.textColor = Color.white;

            errorStyle = new GUIStyle(GUI.skin.GetStyle("Normal"))
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 11
            };
            errorStyle.normal.textColor = Color.red;

            labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"))
            {
                alignment = TextAnchor.UpperCenter,
                fontSize = 14
            };
            labelStyle.normal.textColor = Color.white;

            textStyle = new GUIStyle(GUI.skin.GetStyle("Normal"))
            {
                alignment = TextAnchor.UpperRight,
                fontSize = 10
            };
            textStyle.normal.textColor = Color.white;
        }
        /*********************************** GUI WINDOWS ******************************************/
        private void HyperEditGUI(GUIStyle buttonStyle, GUIStyle labelStyle, GUIStyle errorStyle)
        {
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height
            var butn = 140; // button widths

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
                _guiHyperEditShow = false;

            // Feature Buttons
            GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.8f);
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Satsuma Engine Monitor", buttonStyle))
            {
                _guiSatsumaPartShow = !_guiSatsumaPartShow;
                _guiSatsumaPartEditShow = false;
            }
            horiz = 5;
            vert = vert + hgt + 5;
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Satsuma Fluid Level", buttonStyle))
            {
                _guiSatsumaFluidShow = !_guiSatsumaFluidShow;
                _guiSatsumaFluidEditShow = false;
            }
            horiz = 5;
            vert = vert + hgt + 5;
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Gifu Levels", buttonStyle))
            {
                _guiGifuShow = !_guiGifuShow;
                _guiGifuEditShow = false;
            }
            horiz = 5;
            vert = vert + hgt + 5;
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Moped Levels", buttonStyle))
            {
                _guiMopedShow = !_guiMopedShow;
                _guiMopedEditShow = false;
            }
            horiz = 5;
            vert = vert + hgt + 5;
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Van Levels", buttonStyle))
            {
                _guiVanShow = !_guiVanShow;
                _guiVanEditShow = false;
            }
            horiz = 5;
            vert = vert + hgt + 5;
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Tractor Levels", buttonStyle))
            {
                _guiTractorShow = !_guiTractorShow;
                _guiTractorEditShow = false;
            }
            horiz = 5;
            vert = vert + hgt + 5;
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Purchase Statuses", buttonStyle))
            {
                _guiHyperEditShow = true;
            }
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void SatsumaPartGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiSatsumaPartShow = false;
            }
            if (GUI.Button(new Rect(170, 0, 20, 20), ">", errorStyle))
            {
                _guiSatsumaPartShow = false;
                _guiSatsumaPartEditShow = true;
            }

            // Part Conditions
            PartName = "Alternator";
            if (_wearAlternator != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearAlternator.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Crankshaft";
            if (_wearCrankshaft != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearCrankshaft.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Head Gasket";
            if (_wearHeadgasket != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearHeadgasket.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 1";
            if (_wearPiston1 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston1.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 2";
            if (_wearPiston2 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston2.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 3";
            if (_wearPiston3 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston3.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 4";
            if (_wearPiston4 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston4.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Rockers";
            if (_wearRockershaft != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearRockershaft.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Starter";
            if (_wearStarter != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearStarter.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Water Pump";
            if (_wearWaterpump != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearWaterpump.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Belt";
            if (_wearFanbelt != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), (100 - _wearFanbelt.Value).ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void SatsumaPartEditGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width
            var slid = 345 - (5 + lbl + 5 + val + 5 + typ + 5); // slider width - want it to end 5 from RHS of GUI
            var butn = 340; // button widths

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiSatsumaPartEditShow = false;
            }
            if (GUI.Button(new Rect(330, 0, 20, 20), "<", errorStyle))
            {
                _guiSatsumaPartShow = true;
                _guiSatsumaPartEditShow = false;
            }

            // Part Conditions
            PartName = "Alternator";
            if (_wearAlternator != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearAlternator.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearAlternator.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearAlternator.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Crankshaft";
            if (_wearCrankshaft != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearCrankshaft.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearCrankshaft.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearCrankshaft.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Head Gasket";
            if (_wearHeadgasket != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearHeadgasket.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearHeadgasket.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearHeadgasket.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 1";
            if (_wearPiston1 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston1.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearPiston1.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearPiston1.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 2";
            if (_wearPiston2 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston2.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearPiston2.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearPiston2.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 3";
            if (_wearPiston3 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston3.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearPiston3.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearPiston3.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Piston 4";
            if (_wearPiston4 != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearPiston4.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearPiston4.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearPiston4.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Rockers";
            if (_wearRockershaft != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearRockershaft.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearRockershaft.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearRockershaft.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Starter";
            if (_wearStarter != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearStarter.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearStarter.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearStarter.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Water Pump";
            if (_wearWaterpump != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _wearWaterpump.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearWaterpump.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearWaterpump.Value, 0.0F, 100.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt; // move down one unit
            horiz = 5; // reset horizontal start
            PartName = "Belt";
            if (_wearFanbelt != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Condition: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), (100 - _wearFanbelt.Value).ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "%", textStyle);
                horiz = horiz + typ + 5;
                _wearFanbelt.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _wearFanbelt.Value, 100.0F, 0.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);

            // Bottom Button
            vert = vert + hgt; // move down one unit
            horiz = 5;
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Repair All Parts", buttonStyle))
                FixSatsumaParts();
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void SatsumaFluidGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiSatsumaFluidShow = false;
            }
            if (GUI.Button(new Rect(170, 0, 20, 20), ">", errorStyle))
            {
                _guiSatsumaFluidShow = false;
                _guiSatsumaFluidEditShow = true;
            }
            // Fluid Levels
            PartName = "Oil";
            if (_oilLevel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _oilLevel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Coolant";
            if (_regularCoolant != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _racingCoolant.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else if (_racingCoolant != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _racingCoolant.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Front Brake Fluid";
            if (_brakeFluidF != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _brakeFluidF.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Rear Brake Fluid";
            if (_brakeFluidR != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _brakeFluidR.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Clutch Fluid";
            if (_clutchFluid != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _clutchFluid.Value.ToString("0.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Fuel";
            if (_satsumaFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _satsumaFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void SatsumaFluidEditGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width
            var slid = 345 - (5 + lbl + 5 + val + 5 + typ + 5); // slider width - want it to end 5 from RHS of GUI
            var butn = 340; // button width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiSatsumaFluidEditShow = false;
            }
            if (GUI.Button(new Rect(330, 0, 20, 20), "<", errorStyle))
            {
                _guiSatsumaFluidShow = true;
                _guiSatsumaFluidEditShow = false;
            }
            // Fluid Levels
            PartName = "Oil";
            if (_oilLevel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _oilLevel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _oilLevel.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _oilLevel.Value, 0.0F, 3.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Coolant";
            if (_regularCoolant != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _regularCoolant.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _regularCoolant.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _regularCoolant.Value, 0.0F, 5.4F);
            }
            else if (_racingCoolant != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _racingCoolant.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _racingCoolant.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _racingCoolant.Value, 0.0F, 7.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Front Brake Fluid";
            if (_brakeFluidF != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _brakeFluidF.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _brakeFluidF.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _brakeFluidF.Value, 0.0F, 1.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Rear Brake Fluid";
            if (_brakeFluidR != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _brakeFluidR.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _brakeFluidR.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _brakeFluidR.Value, 0.0F, 1.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Clutch Fluid";
            if (_clutchFluid != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _clutchFluid.Value.ToString("0.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _clutchFluid.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _clutchFluid.Value, 0.0F, 0.5F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Fuel";
            if (_satsumaFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _satsumaFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _satsumaFuel.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _satsumaFuel.Value, 0.0F, 36.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 25; // reset horizontal start

            // Bottom Button
            horiz = 5;
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
            if (GUI.Button(new Rect(horiz, vert, butn, hgt), "Fill All Fluids", buttonStyle))
                FillSatsumaFluids();
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void GifuGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiGifuShow = false;
            }
            if (GUI.Button(new Rect(170, 0, 20, 20), ">", errorStyle))
            {
                _guiGifuShow = false;
                _guiGifuEditShow = true;
            }
            // Gifu Levels
            PartName = "Fuel";
            if (_gifuFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _gifuFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Fuel Oil";
            if (_gifuFuelOil != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _gifuFuelOil.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Human Waste";
            if (_gifuWaste != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _gifuWaste.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void GifuEditGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width
            var slid = 345 - (5 + lbl + 5 + val + 5 + typ + 5); // slider width - want it to end 5 from RHS of GUI

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiGifuEditShow = false;
            }
            if (GUI.Button(new Rect(330, 0, 20, 20), "<", errorStyle))
            {
                _guiGifuShow = true;
                _guiGifuEditShow = false;
            }
            // Fluid Levels
            PartName = "Fuel";
            if (_gifuFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _gifuFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _gifuFuel.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _gifuFuel.Value, 0.0F, 300.0F);
                if (_gifuFuel.Value + _gifuFuelOil.Value > 300)
                {
                    _gifuFuelOil.Value = 300 - _gifuFuel.Value;
                }
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Fuel Oil";
            if (_gifuFuelOil != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _gifuFuelOil.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _gifuFuelOil.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _gifuFuelOil.Value, 0.0F, 300.0F);
                if (_gifuFuel.Value + _gifuFuelOil.Value > 300)
                {
                    _gifuFuel.Value = 300 - _gifuFuelOil.Value;
                }
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            vert = vert + hgt;
            horiz = 5; // reset horizontal start
            PartName = "Human Waste";
            if (_gifuWaste != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _gifuWaste.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _gifuWaste.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _gifuWaste.Value, 0.0F, 10000.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);

            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void MopedGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiMopedShow = false;
            }
            if (GUI.Button(new Rect(170, 0, 20, 20), ">", errorStyle))
            {
                _guiMopedShow = false;
                _guiMopedEditShow = true;
            }
            // Moped Levels
            PartName = "Fuel";
            if (_mopedFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _mopedFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void MopedEditGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width
            var slid = 345 - (5 + lbl + 5 + val + 5 + typ + 5); // slider width - want it to end 5 from RHS of GUI

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiMopedEditShow = false;
            }
            if (GUI.Button(new Rect(330, 0, 20, 20), "<", errorStyle))
            {
                _guiMopedShow = true;
                _guiMopedEditShow = false;
            }
            // Fluid Levels
            PartName = "Fuel";
            if (_mopedFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _mopedFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _mopedFuel.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _mopedFuel.Value, 0.0F, 3.4F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void VanGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiVanShow = false;
            }
            if (GUI.Button(new Rect(170, 0, 20, 20), ">", errorStyle))
            {
                _guiVanShow = false;
                _guiVanEditShow = true;
            }
            // Van Levels
            PartName = "Fuel";
            if (_vanFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _vanFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void VanEditGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width
            var slid = 345 - (5 + lbl + 5 + val + 5 + typ + 5); // slider width - want it to end 5 from RHS of GUI

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiVanEditShow = false;
            }
            if (GUI.Button(new Rect(330, 0, 20, 20), "<", errorStyle))
            {
                _guiVanShow = true;
                _guiVanEditShow = false;
            }
            // Fluid Levels
            PartName = "Fuel";
            if (_vanFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _vanFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _vanFuel.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _vanFuel.Value, 0.0F, 40.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void TractorGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiTractorShow = false;
            }
            if (GUI.Button(new Rect(170, 0, 20, 20), ">", errorStyle))
            {
                _guiTractorShow = false;
                _guiTractorEditShow = true;
            }
            // Tractor Levels
            PartName = "Fuel";
            if (_tractorFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _tractorFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
            }
            else
                GUI.Label(new Rect(horiz, vert, 190, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        private void TractorEditGUI(GUIStyle buttonStyle, GUIStyle errorStyle, GUIStyle labelStyle, GUIStyle textStyle)
        {
            var PartName = "";
            var horiz = 5; // starting horizontal position
            var vert = 20; // default starting point
            var hgt = 20; // row height

            var lbl = 120; // label width
            var val = 40; // value width
            var typ = 10; // Type Indicator Width
            var slid = 345 - (5 + lbl + 5 + val + 5 + typ + 5); // slider width - want it to end 5 from RHS of GUI

            // Top Button
            if (GUI.Button(new Rect(0, 0, 20, 20), "╳", errorStyle))
            {
                _guiTractorEditShow = false;
            }
            if (GUI.Button(new Rect(330, 0, 20, 20), "<", errorStyle))
            {
                _guiTractorShow = true;
                _guiTractorEditShow = false;
            }
            // Fluid Levels
            PartName = "Fuel";
            if (_tractorFuel != null)
            {
                GUI.Label(new Rect(horiz, vert, lbl, hgt), PartName + " Level: ", textStyle);
                horiz = horiz + lbl + 5;
                GUI.Label(new Rect(horiz, vert, val, hgt), _tractorFuel.Value.ToString("#.00"), textStyle);
                horiz = horiz + val + 5;
                GUI.Label(new Rect(horiz, vert, typ, hgt), "L", textStyle);
                horiz = horiz + typ + 5;
                _tractorFuel.Value = GUI.HorizontalSlider(new Rect(horiz, vert, slid, hgt), _tractorFuel.Value, 0.0F, 65.0F);
            }
            else
                GUI.Label(new Rect(horiz, vert, 350, hgt), "Error: " + PartName + " Not found.", errorStyle);
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
        /*********************************** VALUE FUNCTIONS **************************************/
        private void FillSatsumaFluids()
        {
            if (_satsumaFuel != null)
                _satsumaFuel.Value = 36f;
            if (_oilLevel != null)
                _oilLevel.Value = 3f;
            if (_racingCoolant != null)
                _racingCoolant.Value = 7f;
            if (_regularCoolant != null)
                _regularCoolant.Value = 5.4f;
            if (_clutchFluid != null)
                _clutchFluid.Value = 0.5f;
            if (_brakeFluidF != null)
                _brakeFluidF.Value = 1f;
            if (_brakeFluidR != null)
                _brakeFluidR.Value = 1f;
        }
        private void FixSatsumaParts()
        {
            var satsuma = GameObject.Find("SATSUMA(557kg, 248)");
            foreach (var fsm in satsuma.GetComponentsInChildren<PlayMakerFSM>())
                foreach (var floats in fsm.FsmVariables.FloatVariables)
                    if (floats.Name == "WearAlternator" || floats.Name == "WearCrankshaft" ||
                        floats.Name == "WearGearbox" || floats.Name == "WearHeadgasket"
                        || floats.Name == "WearPiston1" || floats.Name == "WearPiston2" || floats.Name == "WearPiston3"
                        || floats.Name == "WearPiston4" || floats.Name == "WearRockershaft" ||
                        floats.Name == "WearStarter" || floats.Name == "WearWaterpump")
                        floats.Value = 100f;

            if (_wearFanbelt != null)
                _wearFanbelt.Value = 0;
        }

        private void GetSatsumaPartStatus()
        {
            if (!_getSatsumaPartValues)
            {
                if (GameObject.Find("SATSUMA(557kg, 248)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.FsmVariables.FindFsmFloat("WearAlternator") != null &&
                            fsm.FsmVariables.FindFsmFloat("WearGearbox") != null &&
                            fsm.FsmVariables.FindFsmFloat("WearCrankshaft") != null && fsmfloat("WearHeadgasket")
                            != null && fsmfloat("WearPiston1") != null
                            && fsmfloat("WearPiston2") != null && fsmfloat("WearPiston3") != null &&
                            fsmfloat("WearPiston4") != null && fsmfloat("WearRockershaft") != null
                            && fsmfloat("WearStarter") != null && fsmfloat("WearWaterpump") != null)
                        {
                            _wearAlternator = fsm.FsmVariables.FindFsmFloat("WearAlternator");
                            _wearCrankshaft = fsm.FsmVariables.FindFsmFloat("WearCrankshaft");
                            _wearGearbox = fsm.FsmVariables.FindFsmFloat("WearGearbox");
                            _wearHeadgasket = fsm.FsmVariables.FindFsmFloat("WearHeadgasket");
                            _wearPiston1 = fsm.FsmVariables.FindFsmFloat("WearPiston1");
                            _wearPiston2 = fsm.FsmVariables.FindFsmFloat("WearPiston2");
                            _wearPiston3 = fsm.FsmVariables.FindFsmFloat("WearPiston3");
                            _wearPiston4 = fsm.FsmVariables.FindFsmFloat("WearPiston4");
                            _wearRockershaft = fsm.FsmVariables.FindFsmFloat("WearRockershaft");
                            _wearStarter = fsm.FsmVariables.FindFsmFloat("WearStarter");
                            _wearWaterpump = fsm.FsmVariables.FindFsmFloat("WearWaterpump");

                            _wearFanbelt = GameObject.Find("Fanbelt").GetComponent<PlayMakerFSM>().FsmVariables
                                .FindFsmFloat("Wear");
                        }
                    }
                    _getSatsumaPartValues = true;
                }
            }
        }
        private void GetSatsumaFluidStatus()
        {
            if (!_getSatsumaFluidValues)
            {
                if (GameObject.Find("SATSUMA(557kg, 248)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.gameObject.transform.root.name == "Database")
                        {
                            if (fsm.gameObject.name == "FuelTank")
                                _satsumaFuel = fsm.FsmVariables.FindFsmFloat("FuelLevel");
                            if (fsm.gameObject.name == "Oilpan")
                                _oilLevel = fsm.FsmVariables.FindFsmFloat("Oil");
                            if (fsm.gameObject.name == "Racing Radiator")
                                _racingCoolant = fsm.FsmVariables.FindFsmFloat("Water");
                            if (fsm.gameObject.name == "Radiator")
                                _regularCoolant = fsm.FsmVariables.FindFsmFloat("Water");
                            if (fsm.gameObject.name == "BrakeMasterCylinder")
                            {
                                _brakeFluidF = fsm.FsmVariables.FindFsmFloat("BrakeFluidF");
                                _brakeFluidR = fsm.FsmVariables.FindFsmFloat("BrakeFluidR");
                            }
                            if (fsm.gameObject.name == "ClutchMasterCylinder")
                                _clutchFluid = fsm.FsmVariables.FindFsmFloat("ClutchFluid");
                        }
                    }
                    _getSatsumaFluidValues = true;
                }
            }
        }
        private void GetSatsumaTuning()
        {
            if (!_getSatsumaFluidValues)
            {
                if (GameObject.Find("SATSUMA(557kg, 248)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.gameObject.transform.root.name == "Database")
                        {
                            if (fsm.gameObject.name == "FuelTank")
                                _satsumaFuel = fsm.FsmVariables.FindFsmFloat("FuelLevel");
                            if (fsm.gameObject.name == "Oilpan")
                                _oilLevel = fsm.FsmVariables.FindFsmFloat("Oil");
                            if (fsm.gameObject.name == "Racing Radiator")
                                _racingCoolant = fsm.FsmVariables.FindFsmFloat("Water");
                            if (fsm.gameObject.name == "Radiator")
                                _regularCoolant = fsm.FsmVariables.FindFsmFloat("Water");
                            if (fsm.gameObject.name == "BrakeMasterCylinder")
                            {
                                _brakeFluidF = fsm.FsmVariables.FindFsmFloat("BrakeFluidF");
                                _brakeFluidR = fsm.FsmVariables.FindFsmFloat("BrakeFluidR");
                            }
                            if (fsm.gameObject.name == "ClutchMasterCylinder")
                                _clutchFluid = fsm.FsmVariables.FindFsmFloat("ClutchFluid");
                        }
                    }
                    _getSatsumaFluidValues = true;
                }
            }
        }

        private void GetGifuStatus()
        {
            if (!_getGifuValues)
            {
                if (GameObject.Find("GIFU(750/450psi)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.gameObject.transform.root.name == "GIFU(750/450psi)")
                        {
                            if (fsm.gameObject.name == "FuelTank")
                                _gifuFuel = fsm.FsmVariables.FindFsmFloat("FuelLevel");
                            if (fsm.gameObject.name == "FuelTank")
                                _gifuFuelOil = fsm.FsmVariables.FindFsmFloat("FuelOil");
                        }
                    }
                }
                if (GameObject.Find("GIFU(750/450psi)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.gameObject.transform.root.name == "ShitTank")
                        {
                            if (fsm.gameObject.name == "Waste")
                                _gifuWaste = fsm.FsmVariables.FindFsmFloat("Waste");
                        }
                    }
                }
                _getGifuValues = true;
            }
        }
        private void GetMopedStatus()
        {
            if (!_getMopedValues)
            {
                if (GameObject.Find("JONNEZ ES(Clone)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        if (fsm.gameObject.name == "FuelTank")
                        {
                            _mopedFuel = fsm.FsmVariables.FindFsmFloat("FuelLevel");
                        }
                    }
                }
                _getMopedValues = true;
            }
        }
        private void GetVanStatus()
        {
            if (!_getVanValues)
            {
                if (GameObject.Find("HAYOSIKO(1500kg, 250)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.gameObject.transform.root.name == "HAYOSIKO(1500kg, 250)")
                        {
                            if (fsm.gameObject.name == "FuelTank")
                                _vanFuel = fsm.FsmVariables.FindFsmFloat("FuelLevel");
                        }
                    }
                }
                _getVanValues = true;
            }
        }
        private void GetTractorStatus()
        {
            if (!_getTractorValues)
            {
                if (GameObject.Find("KEKMET(350-400psi)") != null)
                {
                    foreach (var fsm in Resources.FindObjectsOfTypeAll<PlayMakerFSM>())
                    {
                        var fsmfloat = new Func<string, FsmFloat>(fsm.FsmVariables.FindFsmFloat);

                        if (fsm.gameObject.transform.root.name == "KEKMET(350-400psi)")
                        {
                            if (fsm.gameObject.name == "FuelTank")
                                _tractorFuel = fsm.FsmVariables.FindFsmFloat("FuelLevel");
                        }
                    }
                }
                _getTractorValues = true;
            }
        }
    }
}