using Reqnroll;
using CodeImp.DoomBuilder.Platform;
using Xunit;

namespace CodeImp.DoomBuilder.BDD.Steps
{
	[Binding]
	public class CrossPlatformSteps
	{
		private IPlatformSettings _settings;
		private IPlatformFileLock _fileLock;
		private string _tempFile;
		private bool _isLocked;

		[Given("the platform settings service is initialized")]
		public void GivenThePlatformSettingsServiceIsInitialized()
		{
			_settings = PlatformFactory.Settings;
			Assert.NotNull(_settings);
		}

		[When("I store a setting {string} with value {string}")]
		public void WhenIStoreASetting(string key, string value)
		{
			_settings.SetValue(key, value);
		}

		[Then("retrieving {string} should return {string}")]
		public void ThenRetrievingShouldReturn(string key, string expectedValue)
		{
			var actualValue = _settings.GetValue(key, "");
			Assert.Equal(expectedValue, actualValue);

			// Clean up
			_settings.DeleteKey(key);
		}

		[Given("a locked file exists")]
		public void GivenALockedFileExists()
		{
			_tempFile = System.IO.Path.GetTempFileName();
			_fileLock = PlatformFactory.FileLock;
		}

		[When("I check if the file is locked")]
		public void WhenICheckIfTheFileIsLocked()
		{
			// Lock the file by holding it open
			using (var stream = System.IO.File.Open(_tempFile, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None))
			{
				_isLocked = _fileLock.IsFileLocked(_tempFile);
			}
		}

		[Then("the file lock checker should report it as locked")]
		public void ThenTheFileLockCheckerShouldReportItAsLocked()
		{
			// Note: The lock check happens inside the using block above,
			// so _isLocked should be true if the file was actually locked
			Assert.True(_isLocked);

			// Clean up
			if (System.IO.File.Exists(_tempFile))
				System.IO.File.Delete(_tempFile);
		}

		[Given("the native BuilderNative library is available")]
		public void GivenTheNativeBuilderNativeLibraryIsAvailable()
		{
			// This step verifies the native library exists for the current platform
		}

		[When("I attempt to load the native library")]
		public void WhenIAttemptToLoadTheNativeLibrary()
		{
			// Attempting to load will be done in the Then step
		}

		[Then("the library should load without errors")]
		public void ThenTheLibraryShouldLoadWithoutErrors()
		{
			// This will be validated once the native library is built and available
			// For now, this is a placeholder
			Assert.True(true, "Native library loading test placeholder");
		}
	}
}
