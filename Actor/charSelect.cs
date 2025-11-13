import greenfoot.*;

public class charSelect extends Actor
{
    public charSelect() {
        GreenfootImage img = getImage();
        img.scale(120, 50);
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            charSelectionBg world = (charSelectionBg) getWorld();
            int selectedChar = world.getCurrentCharacter();
            
            boolean canSelect = false;
            
            if (selectedChar == 1)
            {
                canSelect = true;
            }
            else if (selectedChar == 2 && CharacterManager.isPerson2Unlocked())
            {
                canSelect = true;
            }
            else if (selectedChar == 3 && CharacterManager.isPerson3Unlocked())
            {
                canSelect = true;
            }
            
            if (canSelect)
            {
                CharacterManager.setSelectedCharacter(selectedChar);
                Greenfoot.setWorld(new amainMenu());
            }
            else
            {
                world.showText("This character is locked!", 375, 360);
            }
        }
    }
}