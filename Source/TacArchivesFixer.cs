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
using System.Reflection;
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
                this.Log("Start processing");

                Dictionary<string, ScienceSubject> scienceSubjects = GetScienceSubjects();
                if (scienceSubjects != null)
                {
                    List<String> subjects = scienceSubjects.Where(w => w.Value.science == 0.0f).Select(w => w.Key).ToList();
                    foreach (String subject in subjects)
                    {
                        this.LogWarning("Deleting ScienceSubject \"" + subject + "\" because it has zero science.");
                        scienceSubjects.Remove(subject);
                    }

                    this.Log("Done processing");
                }

                done = true;
            }
        }

        void OnDestroy()
        {
            this.Log("OnDestroy");
        }

        private Dictionary<string, ScienceSubject> GetScienceSubjects()
        {
            this.Log("Looking for field of type=" + typeof(Dictionary<string, ScienceSubject>));
            var rnd = ResearchAndDevelopment.Instance;
            var fields = rnd.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Dictionary<string, ScienceSubject>))
                {
                    this.Log("Found it: name=" + field.Name + ", type=" + field.FieldType);
                    return field.GetValue(rnd) as Dictionary<string, ScienceSubject>;
                }
            }

            this.LogWarning("ScienceSubject dictionary not found.");
            return null;
        }
    }
}
