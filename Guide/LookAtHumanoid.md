# LookAtHumanoid

## Overview
`LookAtHumanoid` forces the GameObject it is attached to to continuously rotate and look toward a humanoid avatar in the scene. 

## How to use
1. Attach the `LookAtHumanoid` script to any GameObject you want to orient towards a humanoid avatar.
2. In the Unity Inspector, configure the parameters:
   - **Look At Head**: If checked (true), the object will actively try to look at the Humanoid's head bone. If unchecked, or if the head bone isn't found, it falls back to looking at the root transform of the avatar.
   - **Rotation Speed**: Determines how smoothly the object turns. Set to `0` for an instant snap-look. Higher numbers result in faster, smooth rotation.

## Implementation Details
- It is useful for objects like cameras, spotlights, or NPCs that need to track the user's avatar.
