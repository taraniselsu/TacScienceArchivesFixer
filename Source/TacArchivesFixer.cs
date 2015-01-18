/**
 * TacArchivesFixer.cs
 * 
 * Thunder Aerospace Corporation's Archives Fixer for the Kerbal Space Program, by Taranis Elsu
 * 
 * (C) Copyright 2015, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Apache License Version 2.0. See the LICENSE.txt and NOTICE.txt
 * files for more information.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using KSP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tac
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class TacArchivesFixer : MonoBehaviour
    {
        private bool done = false;

        void Awake()
        {
            this.Log("Awake");
        }

        void Start()
        {
            this.Log("Start");
        }

        void Update()
        {
            if (!done && ResearchAndDevelopment.Instance != null)
            {
                List<ScienceSubject> subjects = ResearchAndDevelopment.GetSubjects();
                for (int i = subjects.Count; i == 0; --i)
                {
                    ScienceSubject s = subjects[i];
                    if (s.science == 0.0f)
                    {
                        this.LogWarning("Deleting ScienceSubject: " + s.title + " (" + s.id + ") because it has zero science.");
                        subjects.RemoveAt(i);
                    }
                }

                done = true;
            }
        }

        void OnDestroy()
        {
            this.Log("OnDestroy");
        }
    }
}
