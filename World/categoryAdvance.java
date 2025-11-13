import greenfoot.*;

public class categoryAdvance extends World {
    public categoryAdvance() {    
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
        advPortal portal1 = new advPortal(1);
        addObject(portal1, 220, 130);
        
        advPortal portal2 = new advPortal(2);
        addObject(portal2, 480, 100);
        
        advPortal portal3 = new advPortal(3);
        addObject(portal3, 620, 200);
        
        advPortal portal4 = new advPortal(4);
        addObject(portal4, 300, 290);
        
        advPortal portal5 = new advPortal(5);
        addObject(portal5, 570, 330);
    }
}