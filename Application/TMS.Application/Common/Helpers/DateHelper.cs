namespace TMS.Application.Common.Helpers;

/// <summary>
/// Provides helper methods for validating <see cref="DateTime"/> and <see cref="DateTimeOffset"/> values.
/// </summary>
public static class DateHelper
{
    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is valid.
    /// A valid date is not <see cref="DateTime.MinValue"/>, <see cref="DateTime.MaxValue"/>, or the default value.
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> value to validate.</param>
    /// <returns><c>true</c> if the date is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidDate(this DateTime date) =>
        date != DateTime.MinValue && date != DateTime.MaxValue && date != default;
    
    /// <summary>
    /// Determines whether the specified <see cref="DateTimeOffset"/> value is valid.
    /// A valid date is not <see cref="DateTimeOffset.MinValue"/>, <see cref="DateTimeOffset.MaxValue"/>, or the default value.
    /// </summary>
    /// <param name="date">The <see cref="DateTimeOffset"/> value to validate.</param>
    /// <returns><c>true</c> if the date is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidDateOffset(this DateTimeOffset date) => 
        date != DateTimeOffset.MinValue && date != DateTimeOffset.MaxValue && date != default;
}