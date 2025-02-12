package dev.stinner.scoutventure.domain.models;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class User {
    private String subject;
    private String firstname;
    private String lastname;
    private String username;
    private String email;
}
