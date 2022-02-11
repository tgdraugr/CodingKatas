import org.junit.jupiter.api.Test
import org.junit.jupiter.api.Assertions.*

internal class PasswordValidationTests {

    @Test
    fun `should register error when password is not at least 8 characters long`(){
        shouldFailWithMessage("!A34567", "Password must be at least 8 characters")
    }

    @Test
    fun `should register error when password does not contain at least 2 numbers`(){
        shouldFailWithMessage("!2bCdefg", "Password must contain at least 2 numbers")
    }

    @Test
    fun `should register error when password does not contain at least 1 capital letter`() {
        shouldFailWithMessage("12a!cdef", "Password must contain at least one capital letter")
    }

    @Test
    fun `should register error when password does not contain at least one special character`() {
        shouldFailWithMessage("12Abcdef", "Password must contain at least one special character")
    }

    @Test
    fun `should handle multiple validation errors`() {
        shouldFailWithMessage("sOm?pas",
            "Password must be at least 8 characters\nPassword must contain at least 2 numbers")
    }

    private fun shouldFailWithMessage(password: String, expectedMessage: String) {
        val result = isValidPassword(password)
        assertFalse(result.valid())
        assertEquals(expectedMessage, result.message())
    }
}