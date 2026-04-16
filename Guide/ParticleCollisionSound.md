# ParticleCollisionSound

## Overview
`ParticleCollisionSound` is designed to play an audio clip whenever particles from a Unity `ParticleSystem` collide with geometry in the scene.

## How to use
1. Attach the `ParticleCollisionSound` script to a GameObject that has a `ParticleSystem`. 
   > Note: The Particle System must have the **Collision** module enabled, and its "Send Collision Messages" checkbox must be ticked in order for this script to register the collisions.
2. The script will automatically require an `AudioSource` component. Add an `AudioSource` to the GameObject and configure its 3D sound settings as desired.
3. In the Inspector, assign an AudioClip to the `collisionSound` field.
4. Configure the `timeThreshold` (default is 0.05 seconds). This prevents clipping/overlapping audio if multiple particles collide simultaneously by enforcing a minimum delay between triggering sounds.

## Requirements
- A `ParticleSystem` component on the same GameObject with Collisions and "Send Collision Messages" enabled.
- An `AudioSource` component.
- A valid `AudioClip` assigned to the script.
