import greenfoot.*;

public class categoryMedium extends World {
    public categoryMedium() {    
        super(750, 400, 1); 
        prepare();
    }
    
    private void prepare() {
        GreenfootImage bg = new GreenfootImage("mainMenuBg.png"); 
        bg.scale(750, 400);
        setBackground(bg);
        
        backButton backBtn = new backButton();
        addObject(backBtn, 50, 390);
        
        // Add 5 portals at fixed positions
        mediumPortal portal1 = new mediumPortal(1);
        addObject(portal1, 200, 150);
        
        mediumPortal portal2 = new mediumPortal(2);
        addObject(portal2, 500, 120);
        
        mediumPortal portal3 = new mediumPortal(3);
        addObject(portal3, 350, 250);
        
        mediumPortal portal4 = new mediumPortal(4);
        addObject(portal4, 600, 280);
        
        mediumPortal portal5 = new mediumPortal(5);
        addObject(portal5, 180, 320);
    }
}