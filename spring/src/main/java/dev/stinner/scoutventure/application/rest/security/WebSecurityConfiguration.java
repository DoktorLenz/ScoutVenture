package dev.stinner.scoutventure.application.rest.security;

import dev.stinner.scoutventure.application.rest.RestActuatorEndpoints;
import dev.stinner.scoutventure.application.rest.RestApiEndpoints;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.web.SecurityFilterChain;

@RequiredArgsConstructor
@Configuration
@EnableMethodSecurity(
        jsr250Enabled = true // allows use of @RolesAllowed
)
public class WebSecurityConfiguration {

    private final GrantedAuthoritiesJwtConverter jwtAuthenticationConverter;

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        // CSRF
        http.csrf(AbstractHttpConfigurer::disable);
        http.headers(httpSecurityHeadersConfigurer ->
                httpSecurityHeadersConfigurer.frameOptions(frameOptionsConfig ->
                        frameOptionsConfig.sameOrigin()
                )
        );

        // AUTHENTICATION
        http
                .httpBasic(AbstractHttpConfigurer::disable)
                .formLogin(AbstractHttpConfigurer::disable)
                .oauth2ResourceServer(httpSecurityOAuth2ResourceServerConfigurer ->
                        httpSecurityOAuth2ResourceServerConfigurer.jwt(jwtConfigurer -> {
                                    jwtConfigurer.jwtAuthenticationConverter(jwtAuthenticationConverter);
                                }
                        )
                );

        // SESSION
        http
                .sessionManagement(httpSecuritySessionManagementConfigurer -> httpSecuritySessionManagementConfigurer
                        .sessionCreationPolicy(SessionCreationPolicy.STATELESS)
                );

        // AUTHORIZATION
        http
                .authorizeHttpRequests(authorize -> authorize
                        .requestMatchers(
                                RestActuatorEndpoints.READINESS,
                                RestActuatorEndpoints.LIVENESS,
                                RestApiEndpoints.V1.CONFIGURATION,
                                RestApiEndpoints.V1.KEYCLOAK,
                                RestApiEndpoints.API_DOCS,
                                RestApiEndpoints.SWAGGER
                        )
                        .permitAll()
                        .anyRequest()
                        .authenticated()
                );

        return http.build();
    }
}
