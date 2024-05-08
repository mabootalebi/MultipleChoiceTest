using Contracts.Enums;


namespace Contracts.DTOs
{
    public record Status(StatusTypeEnum type, string? Message);

    public static class CustomStatuses
    {
        #region Success
        public static readonly Status Success = new(StatusTypeEnum.Success, "با موفقیت انجام شد.");
        #endregion

        #region Errors
        public static readonly Status GeneralErrorOccurred = new(StatusTypeEnum.Error, "عملیات با خطا مواجه شد.");

        public static readonly Status CannotRegisterUserDueToUserWithSameUserNameAlreadyExists = new(StatusTypeEnum.Error, "کاربری با این شناسه کاربری از قبل در سامانه وجود دارد.");
        public static readonly Status ErrorOccuredWhileRegisteringUser = new(StatusTypeEnum.Error, "خطایی در ایجاد کاربر رخ داده است.");

        #endregion
    }
}
