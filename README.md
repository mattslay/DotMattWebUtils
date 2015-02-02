# DotMattWebUtils
HTML Helpers for Razor view input fields styled in Bootstrap 3 markup.

Text input field example:

  @Html.BootstrapTextBoxFor(m => m.Firm.AccountNo, "Account No.").DivWrapper("col-md-3")
  
Select list (Dropdown list) example:

@Html.BootstrapDropDownListFor(m => m.Firm.BaseCurrency, Model.DefaultCurrencySelectList, "Base Currency").DivWrapper("col-md-3")
