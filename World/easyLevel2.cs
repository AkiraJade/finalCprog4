import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class easyLevel2 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class easyLevel2 extends World
{
    private boolean spikeSpawned = false;
    private int spikeCount = 0;
    private int spikeX = 75;
    private int spawnDelay = 2;
    private int spawnTimer = 0;
    private int spikeX2 = 700;
    private int spikeCount2 = 0;
    private int storeX = 0;
    private boolean spikeSpawned2 = false;
        
    /**
     * Constructor for objects of class easyLevel2.
     * 
     */
    public easyLevel2()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1); 
        prepare();
    }
    
    public void act() {
        spikeAppearance();
        nextLevel();
    }
    public void prepare(){
        Platform p  = new Platform(750, 100);
        addObject(p, 375, 350);
        Person  player1 = new Person();
        addObject(player1, 30, 280);
        Spike spike = new Spike();
        easyPortal ePor = new easyPortal(2);
        addObject(ePor, 708, 67);
    }
    
    public void spikeAppearance(){
        java.util.List<Person>players = getObjects(Person.class);
        
        if (players.isEmpty()){
            return;
        }
           Person player = players.get(0);
       int playerX = player.getX();
       storeX = playerX;
        
        if (spikeCount < 40 && playerX >= 95) {
            spawnTimer++;
            if (spawnTimer >= spawnDelay){
                addObject(new Spike(), spikeX, 292);
                spikeX += 6;
                spawnTimer = 0;
                spikeCount++;
            }
            
        }
        
        if (playerX >= 360) {
            addObject(new Spike(), 410, 292);
            spikeSpawned = true;
        }
        
        if (playerX >= 430 && spikeSpawned && spikeCount2 < 42){
            spawnTimer++;
            if (spawnTimer >= spawnDelay) {
                addObject(new Spike(), spikeX2, 292);
                spikeX2 -= 6;
                spawnTimer = 0;
                spikeCount2++;
                spikeSpawned2 = true;
                
            }
        }
        
    
    }
    
    public void nextLevel(){
        if (storeX <= 389 && spikeSpawned2){
            Platform p2 = new Platform(50, 5);
            addObject(p2, 410, 250);
            Platform p3 = new Platform(50, 5);
            addObject(p3, 450, 200);
            Platform p4 = new Platform(50, 5);
            addObject(p4, 490, 150);
           Platform p5 = new Platform(200, 10);
            addObject(p5, 650, 100);
    }
}
}