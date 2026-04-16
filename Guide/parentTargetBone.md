# ParentTargetBone

## Overview
`ParentTargetBone` is a complex binding script that attaches the GameObject to a designated humanoid bone on an avatar. It synchronizes the transform accurately, even considering inverse kinematics (VRIK).

## How to use
1. Attach the script to the GameObject you want to pin to the avatar (e.g., a weapon, an accessory, or a prop).
2. Configure the target:
   - Provide a direct `targetTransform` (optional). If empty, it attempts to find an avatar.
   - Select the `HumanBodyBones` enum (e.g., LeftHand, Head, Spine) you wish to track.
3. Adjust the positional offsets (`offsets`) and rotational offsets (`rotOffsets`) to place the prop perfectly relative to the bone.
4. If the object needs mirroring across an axis, use the `inverseX` or `inverseY` boolean flags.

## Features
- **Fallback Constraints:** Automatically enables an underlying Unity `ParentConstraint` as a fallback if the VRIK hook doesn't apply.
