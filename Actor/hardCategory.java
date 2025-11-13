import greenfoot.*;

public class hardCategory extends Actor
{
    private boolean isLocked;
    
    public hardCategory() {
        updateImage();
    }
    
    private void updateImage() {
        // Hard is locked until Medium category is complete
        isLocked = !LevelProgress.isMediumCategoryComplete();
        
        GreenfootImage img = getImage(); 
        img.scale(170, 50);
        
        if (isLocked) {
            img.setTransparency(100); // Make it semi-transparent
        }
        
        setImage(img);
    }
    
    public void act()
    {
        updateImage();
        
        if (Greenfoot.mouseClicked(this))
        {
            if (isLocked) {
                getWorld().showText("Complete all Medium levels first!", getWorld().getWidth()/2, 50);
            } else {
                Greenfoot.setWorld(new categoryHard());
            }
        }
    }
}