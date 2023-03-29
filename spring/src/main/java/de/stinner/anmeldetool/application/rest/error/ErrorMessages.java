package de.stinner.anmeldetool.application.rest.error;

import lombok.experimental.UtilityClass;

@UtilityClass
public class ErrorMessages {

    @UtilityClass
    public static class C400 {
        public static final String BAD_REQUEST = "Bad request";
        public static final String MALFORMED_REQUEST_BODY = "Request body was malformed.";
        public static final String MALFORMED_REQUEST_URL = "Request URL was malformed.";
        public static final String WRONG_PASSWORD = "Old password is wrong.";
    }

    @UtilityClass
    public static class C404 {
        public static final String COULD_NOT_FIND_RESULT = "Could not find result for requested resource.";
        public static final String EXPIRED_REGISTRATION_ID = "The registration id has expired.";
        public static final String EXPIRED_RESET_ID = "The reset id has expired.";
    }

    @UtilityClass
    public static final class C405 {
        public static final String MEHTOD_NOT_ALLOWED = "HTTP-Method not allowed!";
    }

    @UtilityClass
    public static class C500 {
        public static final String INTERNAL_SERVER_ERROR = "There was an internal error. "
                + "Please contact your admin if this error persists.";
    }

}