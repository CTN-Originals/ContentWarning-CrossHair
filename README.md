# CrossHair
Adds a CrossHair to the center of your screen to indicate where you are aiming.
For suggestions or issues, feel free to open an issue on the [GitHub repository](https://github.com/CTN-Originals/ContentWarning-CrossHair/issues).

![Preview Image](<https://raw.githubusercontent.com/CTN-Originals/ContentWarning-CrossHair/main/resources/preview-image.jpg>)

## Installation
### Manual
1. Download the latest version from the [releases page](https://github.com/CTN-Originals/ContentWarning-CrossHair/releases).
2. Extract the zip file.
3. Move the `BepInEx/plugins/com.ctnoriginals.cw.crosshair.dll` file to `BepInEx/plugins` folder.
4. Move the `BepInEx/config/com.ctnoriginals.cw.crosshair.cfg` file to `BepInEx/config` folder.
5. Launch the game and never wonder where you are aiming again!
### Thunderstore
Install using the Thunderstore Mod Manager: https://thunderstore.io/c/content-warning/p/CTWOriginals/CrossHair/

---

# Configuration
| Option | Description | Default |
| ------ | ----------- | ------- |
| **CrossHairText** | Text to display as crosshair (use \n for new line) | `-  +  -` |
| **CrossHairSize** | Size of the crosshair | `40` |
| **CrossHairShadow** | Whether to display a shadow behind the crosshair | `true` |
| **CrossHairColor** | Color of the crosshair in hexadecimal (Do not include the #) | `ffffff` |
| **CrossHairOpacity** | Opacity of the crosshair (0 to 100)% | `80` |

## To-Do
- [ ] Add crosshair fading on specific events (for example, when aiming the camera it needs to fade to 0%)

## Changelog
See [CHANGELOG.md](https://github.com/CTN-Originals/ContentWarning-CrossHair/blob/main/CHANGELOG.md) for the full changelog.