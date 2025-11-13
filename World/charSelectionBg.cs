import greenfoot.*;
public class charSelectionBg extends World
{
    private int currentCharacter = 1;
    private Actor characterDisplay;
    
    public charSelectionBg()
    {    
        super(750, 400, 1);
        currentCharacter = CharacterManager.getSelectedCharacter();
        prepare();
    }
    
    private void prepare()
    {
        updateCharacterDisplay();
        
        arrowLeft leftArrow = new arrowLeft();
        addObject(leftArrow, 135, 200);
        
        arrowRight rightArrow = new arrowRight();
        addObject(rightArrow, 615, 200);
        
        charSelect selectBtn = new charSelect();
        addObject(selectBtn, 375, 370);
        
        backButton backBtn = new backButton();
        addObject(backBtn, 50, 380);
    }
    
    public void updateCharacterDisplay()
    {
        if (characterDisplay != null)
        {
            removeObject(characterDisplay);
            characterDisplay = null;
        }
        
        // Create a static display version of the character
        switch(currentCharacter)
        {
            case 1:
                characterDisplay = new CharacterDisplayPerson();
                break;
            case 2:
                if (CharacterManager.isPerson2Unlocked())
                {
                    characterDisplay = new CharacterDisplayPerson2();
                }
                else
                {
                    // Show locked Person2 (grayed out or with lock icon)
                    characterDisplay = new CharacterDisplayPerson(); // Still show Person as placeholder
                }
                break;
            case 3:
                if (CharacterManager.isPerson3Unlocked())
                {
                    characterDisplay = new CharacterDisplayPerson3();
                }
                else
                {
                    // Show locked Person3 (grayed out or with lock icon)
                    characterDisplay = new CharacterDisplayPerson(); // Still show Person as placeholder
                }
                break;
        }
        
        if (characterDisplay != null)
        {
            addObject(characterDisplay, 375, 200);
        }
        
        showText("SELECT CHARACTER", 375, 50);
        
        if (currentCharacter == 2 && !CharacterManager.isPerson2Unlocked())
        {
            showText("LOCKED - Complete Easy Mode", 375, 330);
        }
        else if (currentCharacter == 3 && !CharacterManager.isPerson3Unlocked())
        {
            showText("LOCKED - Complete Medium Mode", 375, 330);
        }
        else
        {
            showText("", 375, 330);
        }
    }
    
    public void cycleLeft()
    {
        currentCharacter--;
        if (currentCharacter < 1)
        {
            currentCharacter = 3;
        }
        updateCharacterDisplay();
    }
    
    public void cycleRight()
    {
        currentCharacter++;
        if (currentCharacter > 3)
        {
            currentCharacter = 1;
        }
        updateCharacterDisplay();
    }
    
    public int getCurrentCharacter()
    {
        return currentCharacter;
    }
}