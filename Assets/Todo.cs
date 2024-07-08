public class Todo {
	/* ===================================================================
	 * Brainstorm:
     * ===================================================================
     * 1. Change the colour of a folder
     * -  Right click on a folder and see a list of colours
     * -- Using MenuItems, an editor class that allows you to display menus
     * 1.1 Paint a rect with the background texture on top of the original folder
     * 1.2 Draw a custom / coloured icon
     * 
     * 2. Change the icon of a folder
     * - Right click on the folder and see "Custom Icon..." option
     * 
     * 3. Reset the icon of a folder
     * -- Using MenuItems
     * --- Make sure the user is only able to select folders
     * ---- Use Unity.ObjectPicker 
     * 
     * 4. Save Data
     * - Using two techniques: EditorPrefs and custom json file
     * -- Save data using GUIDs (Global Unique Identifiers)
     * --- Every folder and icon has a GUID
     * ---- Data structure: Dictionary<string, string> where the key is the GUID of the folder and the value is the GUID of the icon
     *
     * ===================================================================
     * ===================================================================
     * ===================================================================
     *
     * 5. Custom Hierarchy Tools - To each object in the hierarchy, add:
     * 5.1 Checkbox to deactivate and activate them
     * 5.2 Info icon that displays information when the cursor hovers over it
     * 5.3 Button to focus on each object
     * 5.4 Button to turn the gameobject into a prefab
     * 5.5 Button to delete the gameobject
     *
     * 6. Batch Rename Tool
     * - Select multiple objects and rename them using a name and starting number
     * -- Example: "Enemy" and "1" will rename the objects to "Enemy_1", "Enemy_2", "Enemy_3", etc
     * --- Use the EditorGuiLayout class
     * 
     * 7. Create a tool that detects missing references in the scene
     * 
     * 8. Auto linking tool
     * - Use the name of gameobjects and fields in the inspect to link them. Reduces time spent dragging fields in the inspector
     * 
     * 9. File Organizer
     * - Automatically move specific types of files into specific folders
     * -- Using the GuiLayoutToolbar class. Also popups and object fields from the EditorGuiLayout class
     * 
     * 10. Auto Save Tool
     * - Editor window that allows you to set a time interval
     * -- Use the EditorPref and EditorSceneManager classes
     * 
     * 11. Favorites Tool
     * - Each object in the hierarchy has a star icon that allows you to mark it as a favorite
     * -- Create a favorites menu that displays all the favorites and allows you to create copies of them
     * 
     * 12. Create a custom tool menu
     * 
     * 13. Art Asset Optimizer
     * - Automatically optimize the settings and file size of art assets with the click of a button
     * 
     * 14. UI Aspect Ratio Test
     * - Automatically test aspect ratios of the game and save a screenshot of each
     *
     * ===================================================================
     * Notes:
     * ===================================================================
     * 1. Name the editor script folder "Editor", so that the scripts aren't built into the final game
     * - https://docs.unity3d.com/Manual/SpecialFolders.html
     * 
     * 2. A serialized object is a class that allows an Editor script to reference any Unity object or component in a completely generic way
     * - https://docs.unity3d.com/ScriptReference/SerializedObject.html
     * 2.1 A serialized property allows us to edit properties on serialized objects (Used in MissingReferenceDetector.cs)
     * - https://docs.unity3d.com/ScriptReference/SerializedProperty.html
     * 
     * 3. 
     */
}
