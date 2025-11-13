import greenfoot.*;

public class amainMenuP extends World {
    public amainMenuP() {    
        super(750, 400, 1); 
        prepare();
    }
    
    private void prepare() {
        GreenfootImage bg = new GreenfootImage("mainMenuBg.png"); 
        bg.scale(750, 400);
        setBackground(bg);
        
        // Add title if you want
        title titleSc = new title();
        addObject(titleSc, 380, 100);
        
        // Add category buttons with same spacing as main menu
        easyCategory easyBtn = new easyCategory();
        addObject(easyBtn, 370, 180);
        
        mediumCategory mediumBtn = new mediumCategory();
        addObject(mediumBtn, 370, 240);
        
        hardCategory hardBtn = new hardCategory();
        addObject(hardBtn, 370, 300);
        
        advanceCategory advanceBtn = new advanceCategory();
        addObject(advanceBtn, 370, 360);
        
        backButton backBtn = new backButton();
        addObject(backBtn, 50, 390);
    }
}