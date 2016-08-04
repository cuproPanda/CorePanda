﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using RimWorld;
using Verse;

namespace CorePanda {

  public class WindowManager : MapComponent {

    CompSunlight sunlightComp = new CompSunlight();

    private List<Building_WindowGlower> windows = new List<Building_WindowGlower>();

    private float cachedSunlight;
    private readonly float overlightRadius = 2.2f;

    private readonly WindowGlow bright = new WindowGlow(new ColorInt(175, 175, 165, 0), 9f, 25f, true);
    private readonly WindowGlow lit_5  = new WindowGlow(new ColorInt(150, 150, 140, 0), 8f, 20f);
    private readonly WindowGlow lit_4  = new WindowGlow(new ColorInt(125, 125, 120, 0), 7f, 15f);
    private readonly WindowGlow lit_3  = new WindowGlow(new ColorInt(100, 100, 95, 0), 6f, 10f);
    private readonly WindowGlow lit_2  = new WindowGlow(new ColorInt(67, 67, 75, 0),    4f, 5f);
    private readonly WindowGlow lit_1  = new WindowGlow(new ColorInt(30, 26, 37, 0),    2f, 1f);
    private readonly WindowGlow dark   = new WindowGlow(new ColorInt(1, 1, 2, 0),       1f, 0f);


    /// <summary> Update the windows every TickRare, as needed </summary>
    public override void MapComponentTick() {
      if (Find.TickManager.TicksGame % 250 == 0) {
        sunlightComp.GetSunlight();

        if (sunlightComp.SimpleFactoredSunlight != cachedSunlight) {
          cachedSunlight = sunlightComp.SimpleFactoredSunlight;

          for (int i = 0; i < windows.Count; i++) {
            windows[i].UpdateGlow(GlowStats());
          } 
        }
      }
    }


    private WindowGlow GlowStats() {
      if (sunlightComp.SimpleFactoredSunlight >= 0.9f) {
        return bright;
      }
      if (sunlightComp.SimpleFactoredSunlight >= 0.72f) {
        return lit_5;
      }
      if (sunlightComp.SimpleFactoredSunlight >= 0.54f) {
        return lit_4;
      }
      if (sunlightComp.SimpleFactoredSunlight >= 0.36f) {
        return lit_3;
      }
      if (sunlightComp.SimpleFactoredSunlight >= 0.18f) {
        return lit_2;
      }
      if (sunlightComp.SimpleFactoredSunlight >= 0.08f) {
        return lit_1;
      }
      return dark;
    }
  }
}
