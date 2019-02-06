namespace AutomationTraining
{
    public class Selectors
    {
        public const string EnterButtonSelector = "a[data-target-popup=\"authorize-form\"]";
        public const string UsernameInputSelector = "input[type=\"text\"][name=\"login\"]";
        public const string PasswordInputSelector = "input[type=\"password\"][name=\"password\"]";
        public const string LoginButton = "input.button.auth__enter[type=\"submit\"]";
        public const string UsernameSpanSelector = "span.uname";
        public const string AuthenticationForm = "div.b-auth-f.b-popup";
    }
}