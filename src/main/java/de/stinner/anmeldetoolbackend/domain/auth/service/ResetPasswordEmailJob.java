package de.stinner.anmeldetoolbackend.domain.auth.service;

import de.stinner.anmeldetoolbackend.domain.mail.service.AuthenticationMailService;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;

@Component
@Slf4j
@RequiredArgsConstructor
public class ResetPasswordEmailJob {
    private final AuthenticationService authenticationService;
    private final AuthenticationMailService mailService;

    @Scheduled(cron = "${anmelde-tool.reset-password.mail.prending.cron}")
    void sendPendingResetPasswordEmails() {
        mailService.sendPendingResetPasswordEmails();
    }

    @Scheduled(cron = "${anmelde-tool.reset-password.cleanup.cron}")
    void cleanupOldResetPasswordRequests() {
        authenticationService.cleanupOldResetPasswordRequests();
    }
}