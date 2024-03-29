package dev.stinner.scoutventure.application.rest.logging;

import dev.stinner.scoutventure.application.rest.RestActuatorEndpoints;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.lang.NonNull;
import org.springframework.scheduling.annotation.ScheduledAnnotationBeanPostProcessor;
import org.springframework.util.ObjectUtils;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;
import org.springframework.web.filter.CommonsRequestLoggingFilter;

import java.security.Principal;

@Slf4j
@Configuration
public class LoggingConfiguration {

    private static final String ANONYMOUS_USER = "Anonymous User";

    @Bean
    public ScheduledAnnotationBeanPostProcessor scheduledAnnotationBeanPostProcessor() {
        return new ScheduledAnnotationBeanPostProcessor();
    }

    @Bean
    public CommonsRequestLoggingFilter requestLoggingFilter() {
        return new CustomCommonsRequestLoggingFilter();
    }

    private static class CustomCommonsRequestLoggingFilter extends CommonsRequestLoggingFilter {

        private StopWatch stopWatch;


        @Override
        protected boolean shouldLog(@NonNull HttpServletRequest request) {
            return true;
        }

        @Override
        @NonNull
        protected void beforeRequest(
                @NonNull HttpServletRequest request,
                @NonNull String message
        ) {
            this.stopWatch = StopWatch.start();
            LoggingTracker.start();

            Principal userPrincipal = request.getUserPrincipal();
            String user = ANONYMOUS_USER;

            if (userPrincipal != null) {
                user = userPrincipal.getName();
            }

            // Do not log probing, as it would just clog up the log.
            if (request.getRequestURI().equals(RestActuatorEndpoints.LIVENESS)
                    || request.getRequestURI().equals(RestActuatorEndpoints.READINESS)) {
                return;
            }

            if (!ObjectUtils.isEmpty(request.getQueryString())) {
                log.info(
                        "Request from user: {}, Method: {}, Path: {}, Query: {}",
                        user, request.getMethod(), request.getRequestURL(), request.getQueryString()
                );
            } else {
                log.info(
                        "Request from user: {}, Method: {}, Path: {}",
                        user, request.getMethod(), request.getRequestURL()
                );
            }

        }

        @Override
        protected void afterRequest(
                @NonNull HttpServletRequest request,
                @NonNull String message
        ) {

            int statusCode = 0;

            ServletRequestAttributes requestAttributes =
                    (ServletRequestAttributes) RequestContextHolder.getRequestAttributes();

            HttpServletResponse response;

            if (requestAttributes != null && (response = requestAttributes.getResponse()) != null) {
                statusCode = response.getStatus();
            }

            Principal userPrincipal = request.getUserPrincipal();
            String user = ANONYMOUS_USER;

            if (userPrincipal != null) {
                user = userPrincipal.getName();
            }

            // Do not log probing, as it would just clog up the log.
            if (request.getRequestURI().equals(RestActuatorEndpoints.LIVENESS)
                    || request.getRequestURI().equals(RestActuatorEndpoints.READINESS)) {
                LoggingTracker.stop();
                return;
            }

            if (!ObjectUtils.isEmpty(request.getQueryString())) {
                log.info(
                        "Finished request from user: {}, Method: {}, Path: {}, Query: {}, HttpCode: {}, Duration: {}ms",
                        user, request.getMethod(), request.getRequestURL(),
                        request.getQueryString(), statusCode, stopWatch.stop()
                );
            } else {
                log.info(
                        "Finished request from user: {}, Method: {}, Path: {}, HttpCode: {}, Duration: {}ms",
                        user, request.getMethod(), request.getRequestURL(),
                        statusCode, stopWatch.stop()
                );
            }


            LoggingTracker.stop();
        }
    }

}
