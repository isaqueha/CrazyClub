using UnityEngine;

public struct ParticleParameters {
    public PrimitiveType type;
    public Birth birth;

    public ParticleParameters(PrimitiveType type, Birth birth) {
        this.type = type;
        this.birth = birth;
    }
}