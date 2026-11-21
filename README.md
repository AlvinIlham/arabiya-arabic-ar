# ARabiya (Arabic) - Augmented Reality Arabic Vocabulary Learning with Contextual RTL Glyph Engine

![Unity Version](https://img.shields.io/badge/Unity-2022.3.38f1-blue.svg?style=for-the-badge&logo=unity)
![AR Framework](https://img.shields.io/badge/AR_SDK-PTC_Vuforia_Engine-orange.svg?style=for-the-badge)
![Text Engine](https://img.shields.io/badge/Typography-ArabicSupport_RTL-green.svg?style=for-the-badge)
![Gesture SDK](https://img.shields.io/badge/Gesture-LeanTouch_Suite-yellow.svg?style=for-the-badge)
![Audience](https://img.shields.io/badge/Target-Young_Children_%26_Beginners-blue.svg?style=for-the-badge)
![Platform](https://img.shields.io/badge/Platform-Android_APK-green.svg?style=for-the-badge&logo=android)

---

## 📖 1. Overview & Technical Typography Challenges
**ARabiya** is an interactive mobile Augmented Reality application engineered to teach foundational Arabic vocabulary (*Mufradat*) to young children, madrasah students, and non-native beginners.

A primary technical barrier in developing Arabic educational software on Unity is the rendering of Arabic typography:
1. **Right-to-Left (RTL) Writing Direction:** Standard text rendering pipelines assume Left-to-Right (LTR) text flow.
2. **Contextual Ligatures & Glyphs:** Arabic letters change shape dynamically depending on position: *Isolated*, *Initial*, *Medial*, or *Final*.
3. **Diacritical Marks (*Tasykil*):** Fathah, kasrah, dhammah, tanwin, and tasydid must be positioned accurately over/under characters without causing line displacement.

Without specialized parsing, Unity renders Arabic text in disjointed, reversed characters. ARabiya solves this by embedding a specialized **ArabicSupport** contextual glyph shaper directly into its UI architecture.

---

## 🛠 2. Deep-Dive Arabic RTL Shaper & UI Audio Carousel

### A. Contextual Arabic Glyph Reconstructor (`ArabicSupport.cs`)
Before text strings are passed to Text/TextMeshPro components, `ArabicSupport.cs` performs syntactic parsing:
- Replaces raw Unicode codepoints with correct cursive ligature glyphs.
- Inverts character arrays to render right-to-left while preserving Hindu-Arabic numerals (1, 2, 3...) in proper mathematical orientation.
- Accurately binds vocalic diacritics (*harakat*) to their base consonant glyphs.

### B. 3-Tier Pronunciation Carousel (`ImageSliderUI.cs`)
Enables students to hear vocabulary spoken at different learning speeds:
```csharp
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SlideElement
{
    [Header("Visual Flashcard")]
    public Sprite slideImage;

    [Header("Audio Tier Breakdown")]
    [Tooltip("Tier 1: Slow, emphasized phonetic pronunciation (makhraj)")]
    public AudioClip sound1;
    [Tooltip("Tier 2: Natural conversational speed pronunciation")]
    public AudioClip sound2;
    [Tooltip("Tier 3: Indonesian / native language translation")]
    public AudioClip sound3;
}

public class ImageSliderUI : MonoBehaviour
{
    public Image displayImage;
    public Button nextButton;
    public Button prevButton;
    public List<SlideElement> slides = new List<SlideElement>();
    // Handles touch swipe transitions and audio routing
}
```

---

## 📚 3. Curricular Vocabulary Matrix
| Thematic Module | Scene Binding | Featured Vocabulary (Arabic / Latin / Translation) | 3D Model & Interactive Mechanics |
|:---|:---|:---|:---|
| **Animals (*Al-Hayawanat*)** | `Animals.unity`, `AR_Animals.unity` | Asadun (Lion), Jamalun (Camel), Fiilun (Elephant), Qiththun (Cat) | Animated 3D animal models with authentic animal sound effects upon card detection. |
| **Human Body (*A'dhaul Jismi*)** | `Body.unity` | Ra'sun (Head), 'Ainun (Eye), Yadun (Hand), Rijlun (Foot) | Anatomical callout pointers with single-touch zoom inspection. |
| **Numbers (*Al-Arqam*)** | `Numbers.unity` | Wahid (1), Itsnan (2), Tsalatsah (3) to 'Asyarah (10) | Kinetic 3D floating numbers with counting sound effects. |
| **Colors (*Al-Alwan*)** | `Colors.unity` | Ahmar (Red), Azraq (Blue), Akhdhar (Green), Asfar (Yellow) | Real-time material color switching on 3D test objects. |
| **Classroom Objects** | `Object.unity` | Qalamun (Pen), Kitabun (Book), Maktabun (Desk), Babun (Door) | 3D school objects manipulable via LeanTouch gestures. |

---

## 🖐 4. 360° Touch Manipulation Mechanics (LeanTouch)
- **Drag Rotate (`Rotasi.cs`):** Seamless 360-degree turntable rotation allowing children to view 3D objects from any vantage point.
- **Pinch to Scale (`scale.cs`):** Intuitive two-finger pinch gesture to enlarge or reduce 3D model size.

---

## 📲 5. Compiled Android Deliverables
Located in the `Arabic/` directory:
- **`ARabiya-13.apk`**: Final stable production release with optimized RTL typography and responsive UI.
- `ARabiya-1.apk` through `ARabiya-12.apk` and `Tes-1.apk`: Milestone development archives.
