import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class easyLevel4 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class easyLevel4 extends World
{

    /**
     * Constructor for objects of class easyLevel4.
     * 
     */
    
    int playerX;
    Platform fake1;
    Platform pushingPF;
    Platform fake2;
    Platform pushingPF2;
    Platform fake3;
    boolean spikeAppear = false;
    boolean pushPerson1 = false;
    boolean pushPerson2 = false;
    boolean pushDown = false;
    boolean pushPerson3 = false;
    boolean pushPerson4 = false;
      
        int spikeCount = 0;
        int spawnTimer = 0;
        int spawnDelay = 2;
        int spikeX = 415;
        
    public easyLevel4()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
         super(750, 400, 1); 
         prepare();
    }
    
    public void prepare(){
        Platform p1 = new Platform(100, 100);
        addObject (p1,50, 350);
        Person  player1 = new Person();
        addObject(player1, 30, 280);
        fake1 = new Platform(50, 100);
        addObject (fake1, 125, 350);
        Platform p2 = new Platform(200, 100);
        addObject (p2, 250, 350);
        pushingPF = new Platform(25, 100);
        addObject (pushingPF, 200, 350);
        Platform p3 = new Platform (100,100);
        addObject (p3, 440, 350);
        fake2 = new Platform (50, 100);
        addObject(fake2, 370, 350);
        pushingPF2 = new Platform(25, 100);
        addObject (pushingPF2, 200, 350);
        Platform p4 = new Platform (300, 100);
        addObject(p4, 600, 350);
        easyPortal ePor = new easyPortal(4);
        addObject(ePor, 704, 270);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    boolean fake3Appear = false;
    
    public void act(){
        platformUp();
    }
    
    public void platformUp(){
        
        java.util.List<Person>players = getObjects(Person.class);
        
        if (players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        
        if(playerX >= 165) {
            this.removeObject(fake1);
            pushPerson1 = true;
        }
        
        if (pushPerson1 && pushingPF.getY() > 280){
            pushingPF.setLocation(pushingPF.getX(), pushingPF.getY() - 10);
            pushPerson2 = true;        
        }
        
        if (pushPerson2 && pushingPF.getY() <= 280 && pushingPF.getX() >= 167) {
            pushingPF.setLocation(pushingPF.getX() - 5, pushingPF.getY());
        }  
        
        if  (pushingPF.getX() <= 166) {
            pushingPF.setLocation(pushingPF.getX(), pushingPF.getY() + 20);
        }
        
        if (playerX >= 246) {
            pushPerson3 = true;
        }
        if (playerX >= 333) {
            this.removeObject(fake2);    
        }
        
        if(pushPerson3 && pushingPF2.getY() > 280){
            pushingPF2.setLocation(pushingPF2.getX(), pushingPF2.getY() - 5);
            pushPerson4 = true;
            
        }
        
        if(pushPerson4 && pushingPF2.getY() <= 280 && pushingPF2.getX() < 330){
            pushingPF2.setLocation(pushingPF2.getX() + 10, pushingPF2.getY());
           
        }
        
        if(pushingPF2.getX() >= 329) {
            pushingPF2.setLocation(pushingPF2.getX(), pushingPF2.getY() + 10);
             spikeAppear = true;
             if(!fake3Appear){
              fake3 = new Platform(25,100);
            addObject(fake3, 403, 250);
            fake3Appear = true;
        }
        }
        
        if (spikeAppear && playerX <= 352) {
            Spike sp = new Spike();
            addObject(sp, 280, 295);
            Spike sp2 = new Spike();
            addObject(sp2, 299, 295);
            Spike sp3 = new Spike();
            addObject(sp3, 318, 295);
         
            
        }
        
        if(spikeAppear && playerX <= 352) {
           this.removeObject(fake3);
         
           //430
        }
      
        if (spikeCount < 53 &&  playerX >= 435) {
            spawnTimer++;
            if (spawnTimer >= spawnDelay){
                addObject(new Spike(), spikeX, 292);
                spikeX += 6;
                spawnTimer = 0;
                spikeCount++;
            }
        } 
        
    }
}