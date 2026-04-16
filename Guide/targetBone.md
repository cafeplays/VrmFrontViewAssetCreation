# TargetBone

## Overview
`targetBone` is a simple constraint configurator used to map and attach an object to one of an avatar's limbs using a Unity `PositionConstraint`.

## How to use
1. Attach `targetBone` to the GameObject you wish to constrain. 
2. Ensure the GameObject also has a `PositionConstraint` component.
3. Configure `boneNum` in the Inspector to control which limb to bind to:
   - `0`: Left Hand
   - `1`: Right Hand
   - `2`: Left Foot
   - `3`: Right Foot
4. Upon execution, the script dynamically locates the active avatar's `Animator`, extracts the relevant `HumanBodyBones` transform, and injects it as a source into the `PositionConstraint`.
