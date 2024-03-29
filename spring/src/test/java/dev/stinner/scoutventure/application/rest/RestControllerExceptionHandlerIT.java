package dev.stinner.scoutventure.application.rest;

import dev.stinner.scoutventure.base.BaseControllerTest;
import dev.stinner.scoutventure.domain.error.ErrorMessages;
import dev.stinner.scoutventure.domain.error.ErrorResponse;
import dev.stinner.scoutventure.domain.error.api.ExceptionHandlerTestController;
import dev.stinner.scoutventure.domain.error.api.TestControllerDto;
import io.restassured.http.ContentType;
import lombok.SneakyThrows;
import org.junit.jupiter.api.Test;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;

import java.util.List;

import static io.restassured.module.mockmvc.RestAssuredMockMvc.given;
import static org.assertj.core.api.Assertions.assertThat;

class RestControllerExceptionHandlerIT extends BaseControllerTest {

    private static void assertErrorResponse(ErrorResponse errorResponse, HttpStatus httpStatus, String errorMessage) {
        assertErrorResponse(errorResponse, httpStatus, errorMessage, List.of());
    }

    private static void assertErrorResponse(ErrorResponse errorResponse, HttpStatus httpStatus, String errorMessage, List<String> details) {
        assertThat(errorResponse.getStatus()).isEqualTo(httpStatus.value());
        assertThat(errorResponse.getErrorMessage()).isEqualTo(errorMessage);
        assertThat(errorResponse.getDetails()).isEqualTo(details);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_tryInvalidHttpMethod_then_405_response() {
        given().contentType(ContentType.JSON)
                .when().post(ExceptionHandlerTestController.EXCEPTION_TEST_ENDPOINT)
                .then().statusCode(HttpStatus.METHOD_NOT_ALLOWED.value());


        given().contentType(ContentType.JSON)
                .when().put(ExceptionHandlerTestController.EXCEPTION_TEST_ENDPOINT)
                .then().statusCode(HttpStatus.METHOD_NOT_ALLOWED.value());


        given().contentType(ContentType.JSON)
                .when().delete(ExceptionHandlerTestController.EXCEPTION_TEST_ENDPOINT)
                .then().statusCode(HttpStatus.METHOD_NOT_ALLOWED.value());
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_requestInvalidMediaType_then_406_response() {
        ErrorResponse errorResponse = given().contentType(ContentType.JSON)
                .accept(MediaType.APPLICATION_XML)
                .when().get(ExceptionHandlerTestController.EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.NOT_ACCEPTABLE)
                .extract().as(ErrorResponse.class);

        assertThat(errorResponse.getStatus()).isEqualTo(HttpStatus.NOT_ACCEPTABLE.value());
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_requestInvalidContentType_then_415_response() {
        ErrorResponse errorResponse = given().contentType(ContentType.MULTIPART)
                .when().post(ExceptionHandlerTestController.UNSUPPORTED_MEDIA_TYPE_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.UNSUPPORTED_MEDIA_TYPE)
                .extract().as(ErrorResponse.class);

        assertThat(errorResponse.getStatus()).isEqualTo(HttpStatus.UNSUPPORTED_MEDIA_TYPE.value());
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_genericExceptionThrown_then_500_responseAndNoDetails() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.GENERIC_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.INTERNAL_SERVER_ERROR)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.INTERNAL_SERVER_ERROR, ErrorMessages.INTERNAL_SERVER_ERROR);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_ResponseStatusExceptionThrown_then_properCodeReturned_500() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.RESPONSE_STATUS_EXCEPTION_TEST_ENDPOINT, Integer.toString(HttpStatus.INTERNAL_SERVER_ERROR.value()))
                .then().status(HttpStatus.INTERNAL_SERVER_ERROR)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.INTERNAL_SERVER_ERROR, ErrorMessages.INTERNAL_SERVER_ERROR);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_ResponseStatusExceptionThrown_then_properCodeReturned_404_noReason() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.RESPONSE_STATUS_EXCEPTION_TEST_ENDPOINT, Integer.toString(HttpStatus.NOT_FOUND.value()))
                .then().status(HttpStatus.NOT_FOUND)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.NOT_FOUND, ErrorMessages.NOT_FOUND);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_requestWithInvalidUrlConstraints_then_400_response() {
        ErrorResponse errorResponse = given().contentType(MediaType.APPLICATION_JSON)
                .when().get(ExceptionHandlerTestController.URL_CONSTRAINT_VIOLATION_EXCEPTION_TEST_ENDPOINT, "violated")
                .then().status(HttpStatus.BAD_REQUEST)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.BAD_REQUEST, ErrorMessages.INVALID_PARAMETERS);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_requestWithInvalidBodyConstraints_then_400_response() {
        TestControllerDto dto = new TestControllerDto();
        dto.setName("violated!");

        ErrorResponse errorResponse = given().contentType(ContentType.JSON).body(dto)
                .when().post(ExceptionHandlerTestController.BODY_CONSTRAINT_VIOLATION_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.BAD_REQUEST)
                .extract().as(ErrorResponse.class);

        assertThat(errorResponse.getDetails()).hasSize(1).containsOnly(
                "Field error on field 'name': rejected value [violated!], because size must be between 0 and 5."
        );
        assertThat(errorResponse.getStatus()).isEqualTo(HttpStatus.BAD_REQUEST.value());
        assertThat(errorResponse.getErrorMessage()).isEqualTo(ErrorMessages.MALFORMED_REQUEST_BODY);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_requestWithMissingBodyConstraints_then_400_response() {
        ErrorResponse errorResponse = given().contentType(ContentType.JSON)
                .when().post(ExceptionHandlerTestController.BODY_CONSTRAINT_VIOLATION_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.BAD_REQUEST)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.BAD_REQUEST, ErrorMessages.MISSING_REQUEST_BODY);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_emptyResultDataAccessException_then_404_response() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.EMPTY_RESULT_DATA_ACCESS_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.NOT_FOUND)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.NOT_FOUND, ErrorMessages.NOT_FOUND);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_noSuchElementException_then_404_response() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.NO_SUCH_ELEMENT_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.NOT_FOUND)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.NOT_FOUND, ErrorMessages.NOT_FOUND);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_accessDeniedException_then_403_response() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.ACCESS_DENIED_EXCEPTION_TEST_ENDPOINT)
                .then().status(HttpStatus.FORBIDDEN)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.FORBIDDEN, ErrorMessages.FORBIDDEN);
    }


    @Test
    @SneakyThrows
    @WithMockUser
    void when_namiLoginFailedException_then_401_response() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.NAMI_LOGIN_FAILED_TEST_ENDPOINT)
                .then().status(HttpStatus.UNAUTHORIZED)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(
                errorResponse,
                HttpStatus.UNAUTHORIZED,
                ErrorMessages.NAMI_LOGIN_FAILED,
                List.of("Benutzer nicht gefunden oder Passwort falsch.")
        );
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_namiAccessViolationException_then_401_response() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.NAMI_ACCESS_VIOLATION_TEST_ENDPOINT)
                .then().status(HttpStatus.FORBIDDEN)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.FORBIDDEN, ErrorMessages.NAMI_ACCESS_VIOLATION);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_namiUnavailableException_then_503_response() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.NAMI_UNAVAILABLE_TEST_ENDPOINT)
                .then().status(HttpStatus.SERVICE_UNAVAILABLE)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.SERVICE_UNAVAILABLE, ErrorMessages.NAMI_UNAVAILABLE);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_ResponseStatusExceptionThrown_then_properCodeReturned_404_withReason() {
        final String reason = "best reason!";

        ErrorResponse errorResponse = given().queryParam("reason", reason)
                .when().get(ExceptionHandlerTestController.RESPONSE_STATUS_EXCEPTION_TEST_ENDPOINT, Integer.toString(HttpStatus.NOT_FOUND.value()))
                .then().status(HttpStatus.NOT_FOUND)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.NOT_FOUND, reason);
    }

    @Test
    @SneakyThrows
    @WithMockUser
    void when_ResponseStatusExceptionThrown_then_properCodeReturned_noSpecificHandling() {
        ErrorResponse errorResponse = given()
                .when().get(ExceptionHandlerTestController.RESPONSE_STATUS_EXCEPTION_TEST_ENDPOINT, Integer.toString(HttpStatus.I_AM_A_TEAPOT.value()))
                .then().status(HttpStatus.I_AM_A_TEAPOT)
                .extract().as(ErrorResponse.class);

        assertErrorResponse(errorResponse, HttpStatus.I_AM_A_TEAPOT, ErrorMessages.DEFAULT_4XX_MESSAGE);
    }
}
