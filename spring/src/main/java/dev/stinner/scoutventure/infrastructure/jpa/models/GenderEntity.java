package dev.stinner.scoutventure.infrastructure.jpa.models;

import dev.stinner.scoutventure.domain.models.Gender;

public enum GenderEntity {
    MALE,
    FEMALE,
    DIVERSE,
    UNDEFINED;

    public static GenderEntity fromDomain(Gender gender) {
        return switch (gender) {
            case MALE -> GenderEntity.MALE;
            case FEMALE -> GenderEntity.FEMALE;
            case DIVERSE -> GenderEntity.DIVERSE;
            case UNDEFINED -> GenderEntity.UNDEFINED;
        };
    }

    public Gender toDomain() {
        return switch (this) {
            case MALE -> Gender.MALE;
            case FEMALE -> Gender.FEMALE;
            case DIVERSE -> Gender.DIVERSE;
            case UNDEFINED -> Gender.UNDEFINED;
        };
    }
}
