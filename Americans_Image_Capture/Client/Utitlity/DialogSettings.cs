using MudBlazor;

namespace Americans_Image_Capture.Client.Utitlity
{
    public static class DialogSettings
    {
        public static DialogOptions DialogOptionsAddEditDelete = new DialogOptions()
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = true,
           
            Position = DialogPosition.Center
        };

        public static DialogOptions DialogOptionsImage = new DialogOptions()
        {
            //MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            CloseButton = true,
         
            Position = DialogPosition.Center
        };

        public static DialogOptions ViewPartImageDialogOptions = new DialogOptions()
        {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            CloseButton = true,
           
            Position = DialogPosition.Center
        };
        public static DialogOptions WipSearchDialogOptions = new DialogOptions()
        {

            
            FullWidth = true,
            CloseButton = true,
          
            Position = DialogPosition.Center
        };
    }
}
